using System;
using UnityEngine;
using Cooking;

namespace Serving
{
    [System.Serializable]
    public class RecipeVariation
    {
        [Header("Recipe")]
        public GameObject recipeObject;
        public float serveTime;

        [Header("Ingredients")]
        public CookableIngredient mainIngredient;
        public GameObject[] toppings;

        [Header("Ticket")]
        public GameObject recipeTicket;
        private OrderTicket ticket;

        [Header("Variation - Main Ingredient")]
        public int mainIngredientAmount;
        public int maxMainIngredientAmount = 2;

        [Header("Variation - Toppings")]
        public int[] toppingAmount;
        public int maxToppingAmount = 5;

        public RecipeVariation(Recipe recipe)
        {
            recipeObject = recipe.recipeObject;
            recipeTicket = recipe.recipeTicket;
            serveTime = recipe.serveTime;

            mainIngredient = recipe.mainIngredient;
            toppings = recipe.toppings;

            mainIngredientAmount = 1;
            toppingAmount = new int[toppings.Length];

            for (int i=0; i<toppings.Length; ++i)
            {
                toppingAmount[i] = 1;
            }
        }

        public void MultiplyMain()
        {
            if (CoinFlip())
            {
                mainIngredientAmount = UnityEngine.Random.Range(2, maxMainIngredientAmount + 1);
            }
        }

        public void MultiplyToppings()
        {
            for (int i=0; i<toppings.Length; ++i)
            {
                if (CoinFlip())
                {
                    toppingAmount[i] = UnityEngine.Random.Range(0, maxToppingAmount + 1);
                }
            }
        }

        private bool CoinFlip()
        {
            return UnityEngine.Random.Range(0, 2) == 0;
        }

        public void CreateVariationTicket()
        {
            ticket = recipeTicket.GetComponent<OrderTicket>();
            ticket.recipe = this;
        }
    }
}