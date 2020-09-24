using System;
using System.Collections.Generic;
using UnityEngine;

namespace Serving
{
    public class RecipeSequenceRandom : MonoBehaviour, RecipeSequence
    {
        private List<Recipe> recipes = new List<Recipe>();
        private List<RecipeVariation> recipeVariations = new List<RecipeVariation>();

        public int seed;
        public int numRecipes;

        private RecipeManager recipeManager;

        public void LoadRecipes()
        {
            // Set seed
            if (seed == 0)
            {
                 seed = DateTime.UtcNow.Millisecond;
            }

            // Generate random recipes
            recipeManager = GetComponent<RecipeManager>();
            int maxRecipes = recipeManager.recipes.Length;

            UnityEngine.Random.InitState(seed);

            for (int i = 0; i < numRecipes; ++i)
            {
                int randomNumber = UnityEngine.Random.Range(0, maxRecipes);
                Recipe randomRecipe = recipeManager.recipes[randomNumber];
                recipes.Add(randomRecipe);
            }

            foreach (Recipe r in recipes)
            {
                RecipeVariation v = r.CreateVariation();
                v.MultiplyMain();
                v.MultiplyToppings();
                v.CreateVariationTicket();

                recipeVariations.Add(v);
            }
        }

        public Recipe[] GetRecipes()
        {
            return recipes.ToArray();
        }

        public RecipeVariation[] GetRecipeVariations()
        {
            return recipeVariations.ToArray();
        }
    }
}