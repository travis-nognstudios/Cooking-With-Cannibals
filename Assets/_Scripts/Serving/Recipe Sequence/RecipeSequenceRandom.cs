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
            recipeManager = GetComponent<RecipeManager>();
            System.Random rnJesus = new System.Random(seed);
            int maxRecipes = recipeManager.recipes.Length;

            for (int i = 0; i < numRecipes; ++i)
            {
                int randomNumber = rnJesus.Next(maxRecipes);
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