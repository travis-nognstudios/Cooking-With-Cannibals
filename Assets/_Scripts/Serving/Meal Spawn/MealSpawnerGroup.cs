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

        private OrderSpawnerGroup orderSpawner;
        private RecipeManager recipeManager;
        private List<string> possibleMainIngredientNames = new List<string>();
        private List<string> mainIngredientsPreparedNames = new List<string>();

        private List<RecipeGroup> queuedRecipeGroups = new List<RecipeGroup>();
        private RecipeGroup matchingQueuedRecipeGroup;

        private List<RecipeVariation> queuedRecipes;
        private GameObject dubiousFood;

        private bool canDoReadyCheck;
        private float readyCheckCooldownTimer;
        private float readyCheckCooldownTime = 1f;

        void Start()
        {
            orderSpawner = gameManager.GetComponent<OrderSpawnerGroup>();
            recipeManager = gameManager.GetComponent<RecipeManager>();
            dubiousFood = gameManager.GetComponent<RecipeManager>().dubiousFood;

            GetPossibleMainIngredientNames();
        }

        void Update()
        {
            // Meal spawning
            if (ReadyCheck() && canDoReadyCheck)
            {
                // Start cooldown
                canDoReadyCheck = false;

                // Get all main ingredients on all meal areas
                GetMainIngredientsPreparedNames();
                
                // Get queued recipe groups from order spawner
                GetQueuedRecipeGroups();

                // Check main ingredients against recipes to see if correct meals prepared
                CheckPreparedMealsAgainstQueuedRecipes();

                // Spawn meals/dubious foods accordingly
                // TODO
                // Pass through rater to check for quality
                if (matchingQueuedRecipeGroup != null)
                {
                    SpawnMeals();
                }
                else
                {
                    SpawnDubiousFoods();
                }
                
            }
            
            // Ready check cooldown timer
            if (!canDoReadyCheck)
            {
                readyCheckCooldownTimer += Time.deltaTime;
                if (readyCheckCooldownTimer > readyCheckCooldownTime)
                {
                    readyCheckCooldownTimer = 0f;
                    canDoReadyCheck = true;
                }
            }
        }
        
        private void GetPossibleMainIngredientNames()
        {
            possibleMainIngredientNames.Clear();
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

        private Recipe GetRecipeFromMainIngredientName(string mainIngredientName)
        {
            Recipe[] recipes = recipeManager.recipes;

            foreach (Recipe recipe in recipes)
            {
                string mainName = recipe.mainIngredient.gameObject.name;
                if (mainName.Equals(mainIngredientName))
                {
                    Debug.Log($"Matching recipe: from {mainIngredientName} to {recipe.recipeObject.name}");
                    return recipe;
                }
            }

            return null;
        }
        
        private void GetMainIngredientsPreparedNames()
        {
            mainIngredientsPreparedNames.Clear();

            foreach (MealArea mealArea in mealAreas)
            {
                List<string> allIngredientNames = mealArea.GetInFoodAreaNames();
                string mainIngredientName = GetMainIngredientFromListOfIngredients(allIngredientNames);
                mealArea.mainIngredientName = mainIngredientName;

                if (!mainIngredientName.Equals(""))
                {
                    mainIngredientsPreparedNames.Add(mainIngredientName);
                }
            }
        }

        private void LogMainIngredientPreparedNames()
        {
            Debug.Log("Prepared main ingredients");
            foreach (string n in mainIngredientsPreparedNames)
            {
                Debug.Log(n);
            }
        }

        private void GetQueuedRecipeGroups()
        {
            queuedRecipeGroups.Clear();
            queuedRecipeGroups = orderSpawner.GetOrderedRecipeGroups();
        }
        
        private List<string> GetPreparedRecipeNames()
        {
            List<string> preparedRecipeNames = new List<string>();

            foreach (string mainIngredientName in mainIngredientsPreparedNames)
            {
                string recipeName = NameOfRecipeFromMainIngredient(mainIngredientName);
                preparedRecipeNames.Add(recipeName);

            }
            
            // Log Prepared Recipe Names
            Debug.Log("Prepared Recipes");
            foreach (string n in preparedRecipeNames)
            {
                Debug.Log($"Prepared recipe: {n}");
            }

            return preparedRecipeNames;
        }

        private void CheckPreparedMealsAgainstQueuedRecipes()
        {
            List<string> preparedRecipeNames = GetPreparedRecipeNames();

            // Get Queued Recipe names and find matching name list
            foreach (RecipeGroup recipeGroup in queuedRecipeGroups)
            {
                List<string> queuedRecipeNames = new List<string>();
                GameObject[] recipeObjects = recipeGroup.recipes;

                foreach (GameObject obj in recipeObjects)
                {
                    queuedRecipeNames.Add(obj.gameObject.name);
                }

                // Log queued recipe names
                foreach (string n in queuedRecipeNames)
                {
                    Debug.Log($"Queued Recipe: {n}");
                }
                
                if (NameListsMatch(preparedRecipeNames, queuedRecipeNames))
                {
                    Debug.Log("Found matching group");
                    matchingQueuedRecipeGroup = recipeGroup;
                }
            }
            
        }

        private void SpawnMeals()
        {
            int tipForOrder = 0;

            orderSpawner.CompleteOrder(matchingQueuedRecipeGroup);

            foreach (MealArea mealArea in mealAreas)
            {
                if (mealArea.mainIngredientName != "")
                {
                    Recipe recipePrepared = GetRecipeFromMainIngredientName(mealArea.mainIngredientName);
                    mealArea.DespawnIngredients();
                    mealArea.Spawn(recipePrepared.recipeObject);
                    
                    int tipForMeal = RateQualityOfPreparedRecipe(mealArea.GetInFoodAreaNames(), recipePrepared);
                    tipForOrder += tipForMeal;
                }
            }

            tipJar.AddTip(tipForOrder);
        }

        private void SpawnDubiousFoods()
        {
            Debug.Log("No matching order found");
        }

        private bool RecipeIsReadyBasedOnRater(RecipeVariation recipe)
        {
            /*
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
            */

            return false;
        }

        private int RateQualityOfPreparedRecipe(List<string> ingredientNames, Recipe recipePrepared)
        {
            return 3;
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

        private string NameOfRecipeFromMainIngredient(string mainIngredientName)
        {
            foreach (Recipe recipe in recipeManager.recipes)
            {
                string mainName = recipe.mainIngredient.gameObject.name;
                string recipeName = recipe.recipeObject.name;

                if (mainName.Equals(mainIngredientName))
                {
                    return recipeName;
                }
            }

            return "";
        }
        
        private bool NameListsMatch(List<string> first, List<string> second)
        {
            if (first.Count != second.Count)
            {
                return false;
            }

            foreach (string elem in first)
            {
                if (!second.Contains(elem))
                {
                    return false;
                }
            }

            return true;
        }
    }
}