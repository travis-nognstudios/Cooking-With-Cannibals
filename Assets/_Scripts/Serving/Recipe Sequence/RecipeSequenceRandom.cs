using System;
using System.Collections.Generic;
using UnityEngine;

namespace Serving
{
    public class RecipeSequenceRandom : MonoBehaviour, RecipeSequence
    {
        private List<Recipe> recipes = new List<Recipe>();
        public int seed;

        public int numRecipes;

        private RecipeManager recipeManager;

        void Start()
        {
            
        }

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
        }

        public Recipe[] GetRecipes()
        {
            return recipes.ToArray();
        }
    }
}