using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using AI;
using LevelManagement;

namespace Serving
{
    public class OrderSpawnerv5 : MonoBehaviour
    {
        [Header("Tickets")]
        public TicketManager ticketManager;
        public float orderFrequency;

        [Header("Orders")]
        public float totalServiceTime;
        public int numOrdersForLevel;
        public MonoBehaviour[] sequences;

        private int ordersCompleted;

        private float timeSinceLastOrder;
        [HideInInspector]
        public float timeSinceServiceStarted;
        private bool serviceOver;

        [Header("Recipes")]
        public RecipeManager recipeManager;

        public SaveState saveState;
        // Logic to start spawning
        private bool spawnAllowed;
        private bool firstSpawnDone;

        private List<Recipe> sequencedRecipes = new List<Recipe>();
        private List<RecipeVariation> sequencedRecipeVariations = new List<RecipeVariation>();
        private int sequenceIndex;
        
        void Start()
        {
            // Get sequenced recipes
            foreach (MonoBehaviour sequenceScript in sequences)
            {
                RecipeSequence seq = sequenceScript as RecipeSequence;
                seq.LoadRecipes();
                Recipe[] seqRecipes = seq.GetRecipes();

                foreach (Recipe recipe in seqRecipes)
                {
                    sequencedRecipes.Add(recipe);
                }
            }

            // Get sequenced recipes - Variations
            foreach (MonoBehaviour sequenceScript in sequences)
            {
                RecipeSequence seq = sequenceScript as RecipeSequence;
                seq.LoadRecipes();
                RecipeVariation[] seqRecipeVariations = seq.GetRecipeVariations();

                foreach (RecipeVariation recipeVariation in seqRecipeVariations)
                {
                    sequencedRecipeVariations.Add(recipeVariation);
                }
            }

            /*
            // DEBUG-START
            Debug.Log($"Total recipes: {sequencedRecipeVariations.Count}");

            foreach (RecipeVariation r in sequencedRecipeVariations)
            {
                int time = System.DateTime.Now.Millisecond;

                Debug.Log($"{time} - Recipe: {r.recipeObject.name}");

                Debug.Log($"{time} - Main: {r.mainIngredient.gameObject} x{r.mainIngredientAmount}");

                for (int i = 0; i < r.toppings.Length; ++i)
                {
                    Debug.Log($"{time} - Topping: {r.toppings[i].name} x{r.toppingAmount[i]}");
                }

                Debug.Log($"{time} - END");
            }
            // DEBUG-END
            */
        }

        void Update()
        {
            // Check end of service
            timeSinceServiceStarted += PauseTimer.DeltaTime();
            bool timedOut = timeSinceServiceStarted > totalServiceTime;
            bool allOrdersCompleted = ticketManager.numTicketsCompleted == numOrdersForLevel;

            if (allOrdersCompleted || timedOut)
            {
                EndService();
            }

            if (spawnAllowed)
            {
                // First spawn
                if (!firstSpawnDone)
                {
                    GenerateNewOrder();
                    firstSpawnDone = true;
                }

                // After first spawn
                else
                {
                    bool allCustomersHaveOrdered = ticketManager.numTicketsSpawned == numOrdersForLevel;
                    if (!allCustomersHaveOrdered)
                    {
                        timeSinceLastOrder += Time.deltaTime;

                        bool orderFrequencyReached = timeSinceLastOrder >= orderFrequency;
                        bool noActiveTickets = ticketManager.HasNoTickets();
                        bool canTakeNewOrder = ticketManager.HasRoomForNewTicket();

                        if ((orderFrequencyReached && canTakeNewOrder) || noActiveTickets)
                        {
                            GenerateNewOrder();
                        }
                    }
                }
            }
        }

        private void GenerateNewOrder()
        {
            Recipe recipe = GetSequencedRecipe();
            RecipeVariation recipeVar = recipe.CreateVariation();

            ticketManager.AddTicket(recipeVar);
            timeSinceLastOrder = 0;
        }

        private Recipe GetSequencedRecipe()
        {
            Recipe r = sequencedRecipes[sequenceIndex];
            sequenceIndex++;
            return r;
        }

        public List<RecipeVariation> GetQueuedRecipes()
        {
            return ticketManager.GetActiveTicketRecipes();
        }

        /*
        private RecipeVariation GetSequencedRecipeVariation()
        {
            RecipeVariation r = sequencedRecipeVariations[sequenceIndex];
            sequenceIndex++;
            return r;
        }
        */

        public void CompleteRecipe(Recipe baseRecipe)
        {
            ticketManager.RemoveTicket(baseRecipe);
        }

        /*
        public void CompleteRecipe(Recipe recipeVar)
        {
            ticketManager.RemoveTicket(recipeVar);
        }
        */

        public void StartSpawning()
        {
            spawnAllowed = true;
        }

        public void StopSpawning()
        {
            spawnAllowed = false;
        }

        public void EndService()
        {
            serviceOver = true;
            StopSpawning();
            ticketManager.RemoveAllTickets();
            saveState.Save();
        }

        public void RemoveAllTickets()
        {
            ticketManager.RemoveAllTickets();
        }

        public bool IsServiceOver()
        {
            return serviceOver;
        }
    }
}