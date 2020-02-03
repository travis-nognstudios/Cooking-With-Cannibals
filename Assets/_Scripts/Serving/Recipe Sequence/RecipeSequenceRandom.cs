using System;
using System.Collections.Generic;

namespace Serving
{
    public class RecipeSequenceRandom : RecipeSequence
    {
        private List<Recipe> recipes = new List<Recipe>();
        public int seed;

        public int numRecipes;

        private RecipeManager recipeManager;

        void Start()
        {
            recipeManager = GetComponent<RecipeManager>();
            int maxRecipes = recipeManager.recipes.Length;

            Random rnJesus = new Random(seed);

            for (int i=0; i<numRecipes; ++i)
            {
                int randomNumber = rnJesus.Next(maxRecipes);
                Recipe randomRecipe = recipeManager.recipes[randomNumber];

                recipes.Add(randomRecipe);
            }
        }

        void Update()
        {
            
        }

        public override Recipe[] GetRecipes()
        {
            return recipes.ToArray();
        }
    }
}