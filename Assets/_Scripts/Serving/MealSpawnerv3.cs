using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Recipes;

namespace Serving
{
    public class MealSpawnerv3 : MonoBehaviour
    {
        public GameObject recipeManager;

        private OrderSpawnerv4 orderSpawner;

        private List<Recipe> queuedRecipes;
        private GameObject dubiousFood;
        private List<GameObject> inBox = new List<GameObject>();

        private GameObject spawnedMeal;
        private float destroyTimer = 0f;
        private const float destroyTime = 3f;
        private bool startedDestroyTimer;

        void Start()
        {
            orderSpawner = recipeManager.GetComponent<OrderSpawnerv4>();
            dubiousFood = recipeManager.GetComponent<RecipeManager>().dubiousFood;
        }

        void Update()
        {
            // Meal spawning
            if (BoxIsClosed())
            {
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

            // Spawned meal destroying
            if (startedDestroyTimer)
            {
                destroyTimer += Time.deltaTime;
                if (destroyTimer > destroyTime)
                {
                    DestroySpawnedMeal();
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            GameObject item = other.gameObject;

            // Only add interactables (default layer)
            if (!inBox.Contains(item) && item.layer == 0)
            {
                inBox.Add(item);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            GameObject item = other.gameObject;
            inBox.Remove(item);
        }

        private bool RecipeIsReady(Recipe recipe)
        {
            List<string> inBoxNames = GetInBoxNames();
            bool containsMainIngredient = false;
            int numToppingsContains = 0;

            string mainIngredientNameShouldBe = recipe.mainIngredient.gameObject.name;
            int numToppingsShouldHave = recipe.toppings.Length;

            if (inBoxNames.Contains(mainIngredientNameShouldBe))
            {
                containsMainIngredient = true;
            }

            foreach(GameObject topping in recipe.toppings)
            {
                if (inBoxNames.Contains(topping.name))
                {
                    numToppingsContains += 1;
                }
            }

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

        private List<string> GetInBoxNames()
        {
            List<string> inBoxNames = new List<string>();
            foreach (GameObject item in inBox)
            {
                if (item != null)
                {
                    inBoxNames.Add(item.name);
                }
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
        }

        private void SpawnMeal(Recipe recipe)
        {
            Collider myCollider = GetComponent<Collider>();
            Vector3 mealSpawnOffset = new Vector3(0, 0.3f, 0);

            spawnedMeal = Instantiate(recipe.recipeObject, myCollider.transform.position + mealSpawnOffset, myCollider.transform.rotation);
            StartDestroyTimer();
        }

        private void SpawnDubiousFood()
        {
            Collider myCollider = GetComponent<Collider>();
            Vector3 mealSpawnOffset = new Vector3(0, 0.1f, 0);

            spawnedMeal = Instantiate(dubiousFood, myCollider.transform.position + mealSpawnOffset, myCollider.transform.rotation);
            StartDestroyTimer();
        }

        private void StartDestroyTimer()
        {
            startedDestroyTimer = true;
        }

        private void StopDestroyTimer()
        {
            startedDestroyTimer = false;
            destroyTimer = 0f;
        }

        private void DestroySpawnedMeal()
        {
            Destroy(spawnedMeal);
            StopDestroyTimer();
        }
    }
}