using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Serving
{
    public class MealSpawnerGroup : MonoBehaviour
    {
        [Header("Scene")]
        public GameObject gameManager;
        public MealArea[] mealAreas;
        public MealReadyCheck readyCheck;
        public TipJar tipJar;

        private OrderSpawnerv5 orderSpawner;
        private List<string> possibleMainIngredientNames = new List<string>();
        private List<string> mainIngredientsPreparedNames = new List<string>();

        private List<RecipeVariation> queuedRecipes;
        private GameObject dubiousFood;

        void Start()
        {
            orderSpawner = gameManager.GetComponent<OrderSpawnerv5>();
            dubiousFood = gameManager.GetComponent<RecipeManager>().dubiousFood;

            GetPossibleMainIngredientNames();
        }

        void Update()
        {
            // Meal spawning
            if (ReadyCheck())
            {
                
                // Get all main ingredients on all meal areas
                GetMainIngredientsPreparedNames();

                // Get queued recipes from order spawner

                // Check main ingredients against queued recipes to see if correct meals prepared
                // Spawn meals/dubios foods accordingly
                // Set tips
                // Cleanup orders





                /*
                GetInFoodAreaItems();
                StartSpawnerCooldown();

                // Check all recipes to see if any match
                Recipe matchingRecipe = null;
                bool foundMatchingRecipe = false;

                queuedRecipes = orderSpawner.GetQueuedRecipes();

                foreach (RecipeVariation queuedRecipe in queuedRecipes)
                {
                    if (RecipeIsReadyBasedOnRater(queuedRecipe))
                    {
                        matchingRecipe = queuedRecipe.baseRecipe;
                        foundMatchingRecipe = true;
                    }
                }

                if (foundMatchingRecipe)
                {
                    DespawnIngredients();
                    SpawnMeal(matchingRecipe);
                    orderSpawner.CompleteRecipe(matchingRecipe);
                }
                else if (GetInFoodAreaNames().Count > 0)
                {
                    DespawnIngredients();
                    SpawnDubiousFood();
                }
                else
                {
                    // Debug.Log("Empty Box and no recipes matched");
                    // DO NOTHING
                }
                */
            }
            
        }
        
        private void GetPossibleMainIngredientNames()
        {
            possibleMainIngredientNames.Clear();

            RecipeManager recipeManager = gameManager.GetComponent<RecipeManager>();
            Recipe[] recipes = recipeManager.recipes;

            foreach (Recipe recipe in recipes)
            {
                string mainIngredientName = recipe.mainIngredient.gameObject.name;
                possibleMainIngredientNames.Add(mainIngredientName);
            }
        }

        private string GetMainIngredientFromListOfIngredients(List<string> ingredientNames)
        {
            foreach (string ingredientName in ingredientNames)
            {
                if (ListContainsName(possibleMainIngredientNames, ingredientName))
                {
                    return ingredientName;
                }
            }

            return "";
        }
        
        private void GetMainIngredientsPreparedNames()
        {
            mainIngredientsPreparedNames.Clear();

            foreach (MealArea mealArea in mealAreas)
            {
                List<string> allIngredientNames = mealArea.GetInFoodAreaNames();
                string mainIngredientName = GetMainIngredientFromListOfIngredients(allIngredientNames);

                if (!mainIngredientName.Equals(""))
                {
                    mainIngredientsPreparedNames.Add(mainIngredientName);
                }
            }
        }

        private bool RecipeIsReadyBasedOnRater(RecipeVariation recipe)
        {
            RecipeRating recipeRater = new RecipeRating(inFoodArea, recipe);
            bool isValidRecipe = recipeRater.GetIsValidRecipe();

            if (!isValidRecipe)
            {
                return false;
            }
            else
            {
                recipeRater.FindMistakes();
                int tipAmount = recipeRater.GetTipAmount();
                tipJar.AddTip(tipAmount);
                return true;
            }
        }

        private bool ReadyCheck()
        {
            return readyCheck.IsReady();
        }
    
        bool ListContainsName(List<string> list, string name)
        {
            foreach (string listItemName in list)
            {
                // Check if one string is a substring of the other
                // This allows for copies of objects to have the number
                // at the end and still be recognized
                if (name.Contains(listItemName) || listItemName.Contains(name))
                {
                    return true;
                }
            }

            return false;
        }
    }
}