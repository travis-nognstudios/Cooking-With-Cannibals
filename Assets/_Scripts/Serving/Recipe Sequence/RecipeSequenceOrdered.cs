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
                RecipeVariation v = fullRecipe.CreateVariation();
                v.MultiplyMain();
                v.MultiplyToppings();
                v.CreateVariationTicket();

                fullRecipes.Add(fullRecipe);
                recipeVariations.Add(v);
            }

            /*
            foreach (Recipe r in fullRecipes)
            {
                RecipeVariation v = r.CreateVariation();
                v.MultiplyMain();
                v.MultiplyToppings();
                v.CreateVariationTicket();
                recipeVariations.Add(v);
            }
            */
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