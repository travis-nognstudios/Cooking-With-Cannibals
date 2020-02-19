using UnityEngine;
using System;
using System.Collections.Generic;

namespace Serving
{
    public class RecipeSequenceOrdered : MonoBehaviour, RecipeSequence
    {
        public GameObject[] recipes;
        private List<Recipe> fullRecipes = new List<Recipe>();
        private List<RecipeVariation> recipeVariations = new List<RecipeVariation>();

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

            foreach (Recipe r in fullRecipes)
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
            return fullRecipes.ToArray();
        }

        public RecipeVariation[] GetRecipeVariations()
        {
            return recipeVariations.ToArray();
        }
    }
}