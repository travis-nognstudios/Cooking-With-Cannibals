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
            recipeManager = GetComponent<RecipeManager>();
            int maxRecipes = recipeManager.recipes.Length;

            System.Random rnJesus = new System.Random(seed);

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

        public Recipe[] GetRecipes()
        {
            return recipes.ToArray();
        }
    }
}