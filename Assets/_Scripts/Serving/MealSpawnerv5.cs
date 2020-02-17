using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Serving
{
    public class MealSpawnerv5 : MonoBehaviour
    {
        [Header("Scene")]
        public GameObject gameManager;
        public Collider foodArea;
        public GameObject box;
        public TipJar tipJar;

        [Header("Timers")]
        public float spawnedMealDestroyTime = 10f;
        public float spawnerCooldownTime = 1f;

        private OrderSpawnerv5 orderSpawner;

        private List<Recipe> queuedRecipes;
        private GameObject dubiousFood;
        private List<GameObject> inBox = new List<GameObject>();

        private GameObject spawnedMeal;


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
                    if (RecipeIsReadyBasedOnRater(queuedRecipe))
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
                    // DO NOTHING
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

        private bool RecipeIsReadyBasedOnRater(Recipe recipe)
        {
            RecipeRating recipeRater = new RecipeRating(inBox, recipe);
            bool isValidRecipe = recipeRater.GetIsValidRecipe();

            if (!isValidRecipe)
            {
                return false;
            }
            else
            {
                recipeRater.FindMistakes();
                int tipAmount = recipeRater.GetTipAmount();
                tipJar.AddTip(tipAmount);
                return true;
            }
        }

        private bool RecipeIsReady(Recipe recipe)
        {
            List<string> inBoxNames = GetInBoxNames();
            bool containsMainIngredient = false;
            int numToppingsContains = 0;

            string mainIngredientNameShouldBe = recipe.mainIngredient.gameObject.name;
            int numToppingsShouldHave = recipe.toppings.Length;

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
            Spawn(recipe.recipeObject);
        }

        private void SpawnDubiousFood()
        {
            Spawn(dubiousFood);
        }

        private void Spawn(GameObject item)
        {
            Collider myCollider = GetComponent<Collider>();

            //box.SetActive(false);
            spawnedMeal = Instantiate(item, myCollider.transform.position, item.transform.rotation);

            FinishedMeal finishedMeal = spawnedMeal.GetComponent<FinishedMeal>();
            finishedMeal.PlayFinishFX();

            //Destroy(spawnedMeal, spawnedMealDestroyTime);
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