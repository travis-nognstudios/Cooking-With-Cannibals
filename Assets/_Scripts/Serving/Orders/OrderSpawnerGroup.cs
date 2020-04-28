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
            //StartCoroutine(SetTableOnDelay(6));
        }

        IEnumerator SetTableOnDelay(float seconds)
        {
            Debug.Log("Setting table");
            yield return new WaitForSeconds(seconds);
            GenerateNewOrder();
        }

        void Update()
        {
            // Orders appear at intervals
            if (ordersGenerated < numOrders)
            {
                orderTimer += Time.deltaTime;
                bool roomForOrder = ticketManager.HasRoomForNewTicket();

                if (orderTimer >= orderInterval && roomForOrder)
                {
                    orderTimer = 0f;
                    GenerateNewOrder();
                }
            }
        }

        private RecipeVariation GetSequencedRecipeVariation()
        {
            RecipeVariation r = sequencedRecipeVariations[sequenceIndex];
            sequenceIndex++;
            return r;
        }

        void GenerateNewOrder()
        {
            int numOrdersForTable = Random.Range(minOrdersPerTable, maxOrdersPerTable + 1);
            List<RecipeVariation> recipes = new List<RecipeVariation>();

            for (int i = 0; i < numOrdersForTable; ++i)
            {
                RecipeVariation recipeVar = GetSequencedRecipeVariation();
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
    }
}