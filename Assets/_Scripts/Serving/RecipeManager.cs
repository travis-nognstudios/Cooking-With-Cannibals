using UnityEngine;
using System.Collections;

namespace Serving
{ 
    public class RecipeManager : MonoBehaviour
    {
        public Recipe[] recipes;
        public GameObject dubiousFood;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

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