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
        [Range(0, 100)]
        public int chanceOfVIP = 10;
        public MonoBehaviour[] sequences;

        [Header("GradePoster")]
        public GradePoster gradePoster;

        private float timeSinceLastOrder;
        [HideInInspector]
        public float timeSinceServiceStarted;
        private bool serviceOver;

        [Header("Recipes")]
        public RecipeManager recipeManager;

        [Header("Savestate")]
        public SaveState saveState;

        // Logic to start spawning
        private bool spawnAllowed;
        private bool firstSpawnDone;

        private List<Recipe> sequencedRecipes = new List<Recipe>();
        private int sequenceIndex;
        
        void Start()
        {
            GetSequencedRecipes();
            gradePoster.SetNumCustomers(numOrdersForLevel);
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

        private void GetSequencedRecipes()
        {
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
        }

        private void GenerateNewOrder()
        {
            Recipe recipe = GetSequencedRecipe();
            RecipeVariation recipeVar = recipe.CreateVariation();

            bool isVIP = false;
            int VIPDiceRoll = Random.Range(0, 100);
            if (VIPDiceRoll <= chanceOfVIP) isVIP = true;

            if (!firstSpawnDone) isVIP = false;

            ticketManager.AddTicket(recipeVar, isVIP);
            timeSinceLastOrder = 0;
        }

        private Recipe GetSequencedRecipe()
        {
            Recipe r = sequencedRecipes[sequenceIndex];
            sequenceIndex++;
            return r;
        }

        public List<TicketPointv2> GetQueuedTickets()
        {
            return ticketManager.GetActiveTickets();
        }

        public void CompleteRecipe(Recipe baseRecipe)
        {
            ticketManager.CompleteTicket(baseRecipe);
        }

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