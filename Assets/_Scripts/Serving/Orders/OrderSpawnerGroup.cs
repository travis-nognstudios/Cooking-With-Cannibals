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

        [Header("Tips")]
        public TipJar tipJar;

        private float orderTimer;
        private float ordersGenerated = 0;
        private Stack<int> orderSizes = new Stack<int>();
        private int numTotalMeals;

        private bool spawnAllowed;
        private bool serviceOver;
        private bool firstSpawnDone;

        private List<Recipe> sequencedRecipes = new List<Recipe>();
        private List<RecipeVariation> sequencedRecipeVariations = new List<RecipeVariation>();
        private int sequenceIndex;

        private void Awake()
        {
            CreateOrderSizes();
            SetTipJarCapacity();
        }

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

        private void CreateOrderSizes()
        {
            for (int i=0; i<numOrders; ++i)
            {
                int numMeals = Random.Range(minOrdersPerTable, maxOrdersPerTable + 1);
                
                orderSizes.Push(numMeals);
                numTotalMeals += numMeals;
            }

            Debug.Log($"Total meals: {numTotalMeals}");
            // Debug.Log($"Order size sequence: {orderSizes.ToArray().ToString()}");
        }

        private void SetTipJarCapacity()
        {
            int fullTip = 3;
            tipJar.capacity = numTotalMeals * fullTip;

            Debug.Log($"Original tipjar cap: {tipJar.capacity}");
            
            // Round down to the nearest 5
            while (tipJar.capacity % 5 != 0)
            {
                tipJar.capacity -= 1;
            }

            Debug.Log($"New tipjar cap: {tipJar.capacity}");
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
            //int numOrdersForTable = Random.Range(minOrdersPerTable, maxOrdersPerTable + 1);
            int numOrdersForTable = orderSizes.Pop();

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