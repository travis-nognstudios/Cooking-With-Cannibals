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

        void Start()
        {
            orderSpawner = recipeManager.GetComponent<OrderSpawnerv4>();
            dubiousFood = recipeManager.GetComponent<RecipeManager>().dubiousFood;
        }

        void Update()
        {
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
                    despawnIngredients();
                    spawnMeal(matchingRecipe);
                    orderSpawner.DespawnTicket(matchingRecipe);
                }
                else if (GetInBoxNames().Count > 0)
                {
                    despawnIngredients();
                    spawnDubiousFood();
                }
                else
                {
                    // Debug.Log("Empty Box and no recipes matched");
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

            if (containsMainIngredient && numToppingsContains == numToppingsShouldHave)
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

        private void despawnIngredients()
        {
            foreach (GameObject item in inBox)
            {
                if (item != null)
                {
                    Destroy(item);
                }
            }
        }

        private void spawnMeal(Recipe recipe)
        {
            Collider myCollider = GetComponent<Collider>();
            Vector3 mealSpawnOffset = new Vector3(0, 0.3f, 0);

            Instantiate(recipe.recipeObject, myCollider.transform.position + mealSpawnOffset, myCollider.transform.rotation);
        }

        private void spawnDubiousFood()
        {
            Collider myCollider = GetComponent<Collider>();
            Vector3 mealSpawnOffset = new Vector3(0, 0.1f, 0);

            Instantiate(dubiousFood, myCollider.transform.position + mealSpawnOffset, myCollider.transform.rotation);
        }
    }
}