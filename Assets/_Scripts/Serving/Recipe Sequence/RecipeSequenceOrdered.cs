using UnityEngine;
using System;
using System.Collections.Generic;

namespace Serving
{
    public class RecipeSequenceOrdered : MonoBehaviour, RecipeSequence
    {
        public GameObject[] recipes;
        private List<Recipe> fullRecipes = new List<Recipe>();

        private RecipeManager recipeManager;

        void Start()
        {
            
        }

        public void LoadRecipes()
        {
            recipeManager = GetComponent<RecipeManager>();

            foreach (GameObject recipeObj in recipes)
            {
                Recipe fullRecipe = recipeManager.GetFullRecipe(recipeObj);
                fullRecipes.Add(fullRecipe);
            }
        }
        
        public Recipe[] GetRecipes()
        {
            return fullRecipes.ToArray();
        }
    }
}