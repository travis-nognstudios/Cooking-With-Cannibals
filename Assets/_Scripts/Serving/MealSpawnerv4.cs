using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Serving
{
    public class MealSpawnerv4 : MonoBehaviour
    {
        public GameObject gameManager;
        public Collider foodArea;

        private OrderSpawnerv5 orderSpawner;

        private List<Recipe> queuedRecipes;
        private GameObject dubiousFood;
        private List<GameObject> inBox = new List<GameObject>();

        private GameObject spawnedMeal;

        public float spawnedMealDestroyTime = 3f;
        public float spawnerCooldownTime = 1f;

        private float spawnerCooldown;
        private bool spawnerOnCooldown;

        void Start()
        {
            orderSpawner = gameManager.GetComponent<OrderSpawnerv5>();
            dubiousFood = gameManager.GetComponent<RecipeManager>().dubiousFood;
        }

        void Update()
        {
            // Meal spawning
            if (BoxIsClosed() && !spawnerOnCooldown)
            {
                GetInBoxItems();
                StartSpawnerCooldown();

                // Check all recipes to see if any match
                Recipe matchingRecipe = new Recipe();
                bool foundMatchingRecipe = false;

                queuedRecipes = orderSpawner.GetQueuedRecipes();
                foreach (Recipe queuedRecipe in queuedRecipes)
                {
                    if (RecipeIsReady(queuedRecipe))
                    {
                        matchingRecipe = queuedRecipe;
                        foundMatchingRecipe = true;
                    }
                }

                if (foundMatchingRecipe)
                {
                    DespawnIngredients();
                    SpawnMeal(matchingRecipe);
                    orderSpawner.DespawnTicket(matchingRecipe);
                }
                else if (GetInBoxNames().Count > 0)
                {
                    DespawnIngredients();
                    SpawnDubiousFood();
                }
                else
                {
                    // Debug.Log("Empty Box and no recipes matched");
                }
            }

            // Spawner cooldown timer
            if (spawnerOnCooldown)
            {
                spawnerCooldown += Time.deltaTime;
                if (spawnerCooldown >= spawnerCooldownTime)
                {
                    StopSpawnerCooldown();
                }
            }
        }

        private bool RecipeIsReady(Recipe recipe)
        {
            List<string> inBoxNames = GetInBoxNames();
            bool containsMainIngredient = false;
            int numToppingsContains = 0;

            string mainIngredientNameShouldBe = recipe.mainIngredient.gameObject.name;
            int numToppingsShouldHave = recipe.toppings.Length;

            // if (inBoxNames.Contains(mainIngredientNameShouldBe))
            if (ListContainsName(inBoxNames, mainIngredientNameShouldBe))
            {
                containsMainIngredient = true;
                //ToDo: Check cookstate
            }

            foreach (GameObject topping in recipe.toppings)
            {
                if (ListContainsName(inBoxNames, topping.name))
                {
                    numToppingsContains += 1;
                    //ToDo: Check cookstate
                }
            }

            //ToDo: Allow multiple of the same ingredient
            if (containsMainIngredient && numToppingsContains == numToppingsShouldHave && inBoxNames.Count == numToppingsShouldHave + 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool BoxIsClosed()
        {
            BoxClose boxCloseScript = GetComponentInChildren<BoxClose>();
            return boxCloseScript.isClosed;
        }

        private void GetInBoxItems()
        {
            inBox.Clear();

            Vector3 center = foodArea.bounds.center;
            Vector3 size = foodArea.bounds.size;
            Vector3 halfSize = new Vector3(size[0] / 2, size[1] / 2, size[2] / 2);
            Quaternion orientation = foodArea.gameObject.transform.rotation;
            LayerMask foodLayer = LayerMask.GetMask("Default");

            Collider[] collidersInBox = Physics.OverlapBox(center, halfSize, orientation, foodLayer);
            foreach (Collider c in collidersInBox)
            {
                GameObject ingredient = c.gameObject;
                if (!inBox.Contains(ingredient))
                {
                    inBox.Add(ingredient);
                }
            }
        }

        private List<string> GetInBoxNames()
        {
            List<string> inBoxNames = new List<string>();
            foreach (GameObject item in inBox)
            {
                inBoxNames.Add(item.name);
            }

            return inBoxNames;
        }

        private void DespawnIngredients()
        {
            foreach (GameObject item in inBox)
            {
                if (item != null)
                {
                    Destroy(item);
                }
            }

            inBox.Clear();
        }

        private void SpawnMeal(Recipe recipe)
        {
            Collider myCollider = GetComponent<Collider>();
            Vector3 mealSpawnOffset = new Vector3(0, 0.3f, 0);

            spawnedMeal = Instantiate(recipe.recipeObject, myCollider.transform.position + mealSpawnOffset, recipe.recipeObject.transform.rotation);
            Destroy(spawnedMeal, spawnedMealDestroyTime);
        }

        private void SpawnDubiousFood()
        {
            Collider myCollider = GetComponent<Collider>();
            Vector3 mealSpawnOffset = new Vector3(0, 0.1f, 0);

            spawnedMeal = Instantiate(dubiousFood, myCollider.transform.position + mealSpawnOffset, myCollider.transform.rotation);
        }

        private void StartSpawnerCooldown()
        {
            spawnerOnCooldown = true;
        }

        private void StopSpawnerCooldown()
        {
            spawnerOnCooldown = false;
            spawnerCooldown = 0f;
        }

        bool ListContainsName(List<string> list, string name)
        {
            foreach (string listItemName in list)
            {
                // Check if one string is a substring of the other
                // This allows for copies of objects to have the number
                // at the end and still be recognized
                if (name.Contains(listItemName) || listItemName.Contains(name))
                {
                    return true;
                }
            }

            return false;
        }
    }
}