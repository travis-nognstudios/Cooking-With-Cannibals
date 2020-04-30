using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using AI;
using LevelManagement;

namespace Serving
{
    public class OrderSpawnerGroup : MonoBehaviour
    {
        [Header("Orders")]
        public RecipeManager recipeManager;
        public MonoBehaviour[] sequences;
        int minOrdersPerTable = 2;
        int maxOrdersPerTable = 3;

        [Header("Tickets")]
        public TicketManagerGroup ticketManager;
        public int numOrders;
        public float orderInterval;

        private float orderTimer;
        private float ordersGenerated = 0;

        private bool spawnAllowed;
        private bool serviceOver;
        private bool firstSpawnDone;

        private List<Recipe> sequencedRecipes = new List<Recipe>();
        private List<RecipeVariation> sequencedRecipeVariations = new List<RecipeVariation>();
        private int sequenceIndex;

        void Start()
        {
            Debug.Log("Group order starting");

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
           
            //StartCoroutine(SetTableOnDelay(3));
        }

        void Update()
        {
            if (spawnAllowed)
            {
                // First order spawn independent of timer
                if (!firstSpawnDone)
                {
                    GenerateNewOrder();
                    firstSpawnDone = true;
                }

                // Timed order spawns
                else if (ordersGenerated < numOrders)
                {
                    orderTimer += Time.deltaTime;
                    bool roomForOrder = ticketManager.HasRoomForNewTicket();

                    if (orderTimer >= orderInterval)
                    {
                        orderTimer = 0f;

                        if (roomForOrder)
                        {
                            GenerateNewOrder();
                        }
                    }
                }
            }
        }

        private RecipeVariation GetSequencedRecipeVariation()
        {
            RecipeVariation r = sequencedRecipeVariations[sequenceIndex];
            sequenceIndex++;
            return r;
        }

        private Recipe GetSequencedRecipe()
        {
            Recipe r = sequencedRecipes[sequenceIndex];
            sequenceIndex++;
            return r;
        }

        void GenerateNewOrder()
        {
            int numOrdersForTable = Random.Range(minOrdersPerTable, maxOrdersPerTable + 1);
            List<RecipeVariation> recipes = new List<RecipeVariation>();

            for (int i = 0; i < numOrdersForTable; ++i)
            {
                Recipe r = GetSequencedRecipe();
                RecipeVariation recipeVar = r.CreateVariation();
                recipes.Add(recipeVar);
            }

            ticketManager.AddGroup(recipes);
            ordersGenerated++;
        }

        public List<RecipeGroup> GetOrderedRecipeGroups()
        {
            return ticketManager.GetOrderedRecipeGroups();
        }
        
        public void CompleteOrder(RecipeGroup recipeGroup)
        {
            ticketManager.RemoveGroup(recipeGroup);
        }

        public bool IsServiceOver()
        {
            return serviceOver;
        }

        public void StartSpawning()
        {
            spawnAllowed = true;
        }
    }
}