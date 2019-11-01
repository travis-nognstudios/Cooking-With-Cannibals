using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cooking;
using UnityEngine.UI;

namespace Serving
{
    public class Serve : MonoBehaviour
    {
        public Cookable cookable;
        private Recipe[] recipes;
        private List<Cookable> mealItemScripts = new List<Cookable>();
        private List<RecipeItem> mealItems;

        private int mistakes;
        private int stars;
        public GameObject welcomeText;
        public GameObject winText;
        public GameObject loseText;

        // Use this for initialization
        void Start()
        {
            recipes = GetComponents<Recipe>();

            //TODO: Get mealItemScripts from the world
        

            foreach(Cookable mealItemScript in mealItemScripts)
            {
                RecipeItem item = new RecipeItem();
                item.ingredient = mealItemScript.ingredientType;
                item.cookType = mealItemScript.cookType;
                item.cookState = mealItemScript.GetCookState();

                mealItems.Add(item);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        void MatchMealItemsWithRecipes()
        {
            foreach(Recipe recipe in recipes)
            {
                if (!mealItems.Contains(recipe.ingredient))
                {
                    mistakes += 1;
                }

                foreach(RecipeItem topping in recipe.toppings)
                {
                    if (!mealItems.Contains(topping))
                        {
                            mistakes += 1;
                        }
                }
                if (mealItems.Contains(recipe.ingredient))
                {
                    Debug.Log("Meal Done");
                }
            }//TODO: despawn ingredients spawn meal
        }

        private void OnTriggerEnter(Collider other)
        {
            //*if (other.gameObject.GetComponent<Cookable>())
            //{
             //   mealItemScripts.Add(other.gameObject.GetComponent<Cookable>());
             //   Debug.Log(mealItemScripts);
            //}
           // else
            //{
            //    Debug.Log("Not cookable");
            //}
            

        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<Cookable>())
            {
                
                Debug.Log("Cookable");
             
                mealItemScripts.Add(collision.gameObject.GetComponent<Cookable>());
                Debug.Log(mealItemScripts);
                Debug.Log(mealItemScripts.Count);

                if (cookable.IsCooked())
                {
                    welcomeText.SetActive(false);
                    winText.SetActive(true);
                }
                else
                {
                    welcomeText.SetActive(false);
                    loseText.SetActive(true);
                }
            }
            
        }
        void RankMeal()
        {
            if (mistakes == 0)
            {
                stars = 3;
            }
            else if (mistakes == 1)
            {
                stars = 2;
            }
            else
            {
                stars = 1;
            }
        }

        public int GetRating()
        {
            return stars;
        }
    }
}