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

        [Header("Timers")]
        public float spawnedMealDestroyTime = 10f;
        public float spawnerCooldownTime = 1f;

        private OrderSpawnerv5 orderSpawner;

        private List<TicketPointv2> queuedTickets;
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
            CheckForSpawnedMeals();
            ManageSpawnerCooldown();
        }

        private void CheckForSpawnedMeals()
        {
            if (MealIsReady() && !spawnerOnCooldown)
            {
                GetInFoodAreaItems();
                StartSpawnerCooldown();

                // Check all recipes to see if any match
                Recipe matchingRecipe = null;
                bool foundMatchingRecipe = false;

                queuedTickets = orderSpawner.GetQueuedTickets();

                foreach (TicketPointv2 ticket in queuedTickets)
                {
                    RecipeVariation recipe = ticket.recipe;
                    if (RecipeIsReadyBasedOnRater(recipe))
                    {
                        // Each matching ticket gets a tip amount evaluation.
                        // So, the ticket that is ultimately matched against
                        // the order will know the correct tip evaluation
                        // for that order
                        ticket.recipeTipAmount = GetTipAmountBasedOnRater(recipe);

                        matchingRecipe = recipe.baseRecipe;
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
                    // Empty Box -> DO NOTHING
                }
            }
        }

        private void ManageSpawnerCooldown()
        {
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
            return recipeRater.GetIsValidRecipe();
        }

        private int GetTipAmountBasedOnRater(RecipeVariation recipe)
        {
            RecipeRating recipeRater = new RecipeRating(inFoodArea, recipe);
            recipeRater.FindMistakes();
            return recipeRater.GetTipAmount();
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

    }
}