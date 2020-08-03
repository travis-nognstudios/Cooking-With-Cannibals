using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Serving;

namespace LemonAidRush
{
    public class RushMealSpawner : MonoBehaviour
    {
        [Header("Managers")]
        public RushOrderSpawner orderSpawner;
        public RushRecipeManager recipeManager;

        [Header("Scene")]
        public Collider foodArea;
        public Transform spawnPoint;
        public MealReadyCheck readyCheck;

        [Header("Timers")]
        public float spawnedMealDestroyTime = 3f;
        public float spawnerCooldownTime = 1f;

        private List<RecipeVariation> queuedRecipes;
        private List<GameObject> inFoodArea = new List<GameObject>();

        private GameObject spawnedMeal;

        private float spawnerCooldown;
        private bool spawnerOnCooldown;

        void Start()
        {

        }

        void Update()
        {
            // Meal spawning
            if (MealIsReady() && !spawnerOnCooldown)
            {
                GetInFoodAreaItems();
                StartSpawnerCooldown();

                Recipe matchingRecipe = GetMatchingRecipe();
                SpawnMeal(matchingRecipe);
                DespawnIngredients();
                orderSpawner.MadeRecipe(matchingRecipe);
            }

            ManageSpawnerCooldown();
        }

        private Recipe GetMatchingRecipe()
        {
            Recipe[] recipes = recipeManager.recipes;

            foreach (Recipe recipe in recipes)
            {
                if (RecipeMatchesPreparedMeal(recipe))
                {
                    return recipe;
                }  
            }

            return recipeManager.recipes[0];
        }

        private bool RecipeMatchesPreparedMeal(Recipe recipe)
        {
            string mainIngredientShouldBe = recipe.mainIngredient.gameObject.name;
            List<string> itemsInFoodArea = GetInFoodAreaNames();

            return itemsInFoodArea.Contains(mainIngredientShouldBe);
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
        
        private void Spawn(GameObject item)
        {
            spawnedMeal = Instantiate(item, spawnPoint.position, item.transform.rotation);

            // FinishedMeal finishedMeal = spawnedMeal.GetComponent<FinishedMeal>();
            // finishedMeal.PlayFinishFX();

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
    }
}