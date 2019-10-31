using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cooking;

namespace Serving
{
    public class Serve : MonoBehaviour
    {

        private Recipe[] recipes;
        private List<Cookable> mealitemscripts;
        private List<Recipeitem> mealitems;

        private int mistakes;
        private int stars;

        // Use this for initialization
        void Start()
        {
            recipes = GetComponents<Recipe>();

            //TODO: Get mealitemscripts from the world

            foreach(Cookable mealitemscript in mealitemscripts)
            {
                Recipeitem item = new Recipeitem();
                item.ingredient = mealitemscript.ingredientType;
                item.cooktype = mealitemscript.cooktype;
                item.cookstate = mealitemscript.GetCookstate();

                mealitems.Add(item);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        void MatchMealitemsWithRecipes()
        {
            foreach(Recipe recipe in recipes)
            {
                if (!mealitems.Contains(recipe.ingredient))
                {
                    mistakes += 1;
                }

                foreach(Recipeitem topping in recipe.toppings)
                {
                    if (!mealitems.Contains(topping))
                        {
                            mistakes += 1;
                        }
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