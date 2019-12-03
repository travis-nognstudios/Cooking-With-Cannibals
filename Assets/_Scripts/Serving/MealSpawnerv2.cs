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
        public GameObject mealBox;

        private Recipe myRecipe;
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


        }

        void Update()
        {
            if (recipeIsReady())
            {
                despawnIngredients();
                spawnMeal();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            GameObject item = other.gameObject;
            if (!inBox.Contains(item))
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
                    Debug.Log(topping.name);
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
    }
}