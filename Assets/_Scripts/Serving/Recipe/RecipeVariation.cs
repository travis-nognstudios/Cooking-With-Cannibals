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
        public OrderTicket recipeTicket;

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
            recipeTicket.recipe = this;
        }

        public bool Equals(RecipeVariation other)
        {
            bool baseRecipeMatches = EqualsBaseRecipe(other);

            int numToppings = toppings.Length;
            bool toppingNumberMatches = other.toppings.Length == numToppings;

            bool mainIngredientAmountMatches = other.mainIngredientAmount == mainIngredientAmount;

            bool toppingAmountsMatch = true;
            if (toppingNumberMatches)
            {
                for (int i=0; i<numToppings; ++i)
                {
                    if (other.toppingAmount[i] != toppingAmount[i])
                    {
                        toppingAmountsMatch = false;
                        break;
                    }
                }
            }

            bool perfectMatch = baseRecipeMatches &&
                                mainIngredientAmountMatches &&
                                toppingAmountsMatch;

            return perfectMatch;
        }

        public bool EqualsBaseRecipe(RecipeVariation other)
        {
            bool nameMatches = other.recipeObject.name == recipeObject.name;
            bool mainIngredientMatches = other.mainIngredient.gameObject.name == mainIngredient.gameObject.name;
            bool toppingNumberMatches = other.toppings.Length == toppings.Length;
            int numToppings = toppings.Length;

            bool allToppingsMatch = true;
            if (toppingNumberMatches)
            {
                for (int i = 0; i < numToppings; ++i)
                {
                    if (other.toppings[i].gameObject.name != toppings[i].name)
                    {
                        allToppingsMatch = false;
                        break;
                    }
                }
            }

            bool baseMatches = nameMatches &&
                                mainIngredientMatches &&
                                allToppingsMatch;

            return baseMatches;
        }
    }
}