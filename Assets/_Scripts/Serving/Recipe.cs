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

            RecipeVariation variation = new RecipeVariation();
            MultiplyMain(variation);
            MultiplyToppings(variation);

            return variation;
        }

        public void MultiplyMain(RecipeVariation variation)
        {
            if (CoinFlip())
            {
                variation.mainIngredientAmount = UnityEngine.Random.Range(2, variation.mainIngredientAmount);
            }
            else
            {
                variation.mainIngredientAmount = 1;
            }
        }

        public void MultiplyToppings(RecipeVariation variation)
        {
            int numToppings = toppings.Length;
            variation.toppingAmount = new int[numToppings];

            for (int i=0; i<numToppings; ++i)
            {
                if (CoinFlip())
                {
                    variation.toppingAmount[i] = UnityEngine.Random.Range(2, variation.maxToppingAmount);
                }
                else
                {
                    variation.toppingAmount[i] = 1;
                }
            }
        }

        private bool CoinFlip()
        {
            return UnityEngine.Random.Range(0, 2) == 0;
        }
    }
}