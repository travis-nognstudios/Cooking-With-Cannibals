using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Recipes;

namespace Serving
{
    public class MealSpawnerv2 : MonoBehaviour
    {
        public GameObject recipeManager;
        public GameObject meal;
        

        private Recipe myRecipe;
        private GameObject dubiousFood;
        private List<GameObject> inBox = new List<GameObject>();

        void Start()
        {
            Recipe[] recipes = recipeManager.GetComponent<RecipeManager>().recipes;
            foreach(Recipe recipe in recipes)
            {
                if (recipe.recipeObject.Equals(meal))
                {
                    myRecipe = recipe;
                }
                else
                {
                    myRecipe = new Recipe();
                }
            }

            dubiousFood = recipeManager.GetComponent<RecipeManager>().dubiousFood;
        }

        void Update()
        {
            if (BoxIsClosed())
            {
                despawnIngredients();

                if (recipeIsReady())
                {
                    spawnMeal();
                }
                else
                {
                    spawnDubiousFood();
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

        private bool recipeIsReady()
        {
            List<string> inBoxNames = GetInBoxNames();
            bool containsMainIngredient = false;
            int numToppingsContains = 0;

            string mainIngredientNameShouldBe = myRecipe.mainIngredient.gameObject.name;
            int numToppingsShouldHave = myRecipe.toppings.Length;

            if (inBoxNames.Contains(mainIngredientNameShouldBe))
            {
                containsMainIngredient = true;
            }

            foreach(GameObject topping in myRecipe.toppings)
            {
                if (inBoxNames.Contains(topping.name))
                {
                    numToppingsContains += 1;
                }
            }

            // ToDo: Plug in rating/tip system here
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
                inBoxNames.Add(item.name);
            }

            return inBoxNames;
        }

        private void despawnIngredients()
        {
            foreach (GameObject item in inBox)
            {
                Destroy(item);
            }
        }

        private void spawnMeal()
        {
            Collider myCollider = GetComponent<Collider>();
            Vector3 mealSpawnOffset = new Vector3(0, 0.1f, 0);

            Instantiate(meal, myCollider.transform.position + mealSpawnOffset, myCollider.transform.rotation);
        }

        private void spawnDubiousFood()
        {
            Collider myCollider = GetComponent<Collider>();
            Vector3 mealSpawnOffset = new Vector3(0, 0.1f, 0);

            Instantiate(dubiousFood, myCollider.transform.position + mealSpawnOffset, myCollider.transform.rotation);
        }
    }
}