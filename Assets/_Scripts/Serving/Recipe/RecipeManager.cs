using UnityEngine;
using System.Collections;

namespace Serving
{ 
    public class RecipeManager : MonoBehaviour
    {
        public Recipe[] recipes;
        public GameObject dubiousFood;


        public Recipe GetFullRecipe(GameObject finishedMeal)
        {
            foreach (Recipe recipe in recipes)
            {
                if (recipe.recipeObject == finishedMeal)
                {
                    return recipe;
                }
            }

            return new Recipe();
        }
    }
}