using System;
using UnityEngine;
using Cooking;

namespace Serving
{
    [System.Serializable]
    public class Recipe
    {
        [Header("Recipe")]
        public GameObject recipeObject;
        public GameObject recipeTicket;
        public float serveTime;

        [Header("Ingredients")]
        public CookableIngredient mainIngredient;
        public GameObject[] toppings;

        public RecipeVariation CreateVariation()
        {
            int seed = DateTime.Now.Millisecond;
            UnityEngine.Random.InitState(seed);

            RecipeVariation variation = new RecipeVariation(this);
            return variation;
        }
    }
}