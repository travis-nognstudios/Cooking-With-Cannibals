using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Serving
{
    public class MealSpawnerv5 : MonoBehaviour
    {
        [Header("Scene")]
        public GameObject gameManager;
        public Collider foodArea;
        public Transform spawnPoint;
        public MealReadyCheck readyCheck;
        public TipJar tipJar;

        [Header("Timers")]
        public float spawnedMealDestroyTime = 10f;
        public float spawnerCooldownTime = 1f;

        private OrderSpawnerv5 orderSpawner;

        private List<RecipeVariation> queuedRecipes;
        private GameObject dubiousFood;
        private List<GameObject> inFoodArea = new List<GameObject>();

        private GameObject spawnedMeal;

        private float spawnerCooldown;
        private bool spawnerOnCooldown;

        void Start()
        {
            orderSpawner = gameManager.GetComponent<OrderSpawnerv5>();
            dubiousFood = gameManager.GetComponent<RecipeManager>().dubiousFood;
        }

        void Update()
        {
            // Meal spawning
            if (MealIsReady() && !spawnerOnCooldown)
            {
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
            }

            // Spawner cooldown timer
            if (spawnerOnCooldown)
            {
                spawnerCooldown += Time.deltaTime;
                if (spawnerCooldown >= spawnerCooldownTime)
                {
                    StopSpawnerCooldown();
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

        private bool MealIsReady()
        {
            return readyCheck.IsReady();
        }

        private void GetInFoodAreaItems()
        {
            inFoodArea.Clear();

            Vector3 center = foodArea.bounds.center;
            Vector3 size = foodArea.bounds.size;
            Vector3 halfSize = new Vector3(size[0] / 2, size[1] / 2, size[2] / 2);
            Quaternion orientation = foodArea.gameObject.transform.rotation;
            LayerMask foodLayer = LayerMask.GetMask("Default");

            Collider[] collidersInBox = Physics.OverlapBox(center, halfSize, orientation, foodLayer);
            foreach (Collider c in collidersInBox)
            {
                GameObject ingredient = c.gameObject;

                if (!inFoodArea.Contains(ingredient))
                {
                    inFoodArea.Add(ingredient);
                }
            }
        }

        private List<string> GetInFoodAreaNames()
        {
            List<string> inFoodAreaNames = new List<string>();
            foreach (GameObject item in inFoodArea)
            {
                inFoodAreaNames.Add(item.name);
            }

            return inFoodAreaNames;
        }

        private void DespawnIngredients()
        {
            foreach (GameObject item in inFoodArea)
            {
                if (item != null)
                {
                    Destroy(item);
                }
            }

            inFoodArea.Clear();
        }

        private void SpawnMeal(Recipe recipe)
        {
            Spawn(recipe.recipeObject);
        }

        private void SpawnDubiousFood()
        {
            Spawn(dubiousFood);
        }

        private void Spawn(GameObject item)
        {
            spawnedMeal = Instantiate(item, spawnPoint.position, item.transform.rotation);

            FinishedMeal finishedMeal = spawnedMeal.GetComponent<FinishedMeal>();
            finishedMeal.PlayFinishFX();

            Destroy(spawnedMeal, spawnedMealDestroyTime);
        }

        private void StartSpawnerCooldown()
        {
            spawnerOnCooldown = true;
        }

        private void StopSpawnerCooldown()
        {
            spawnerOnCooldown = false;
            spawnerCooldown = 0f;
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