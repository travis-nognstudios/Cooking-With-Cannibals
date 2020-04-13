﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using AI;
using LevelManagement;

namespace Serving
{
    public class OrderSpawnerGroup : MonoBehaviour
    {
        /*
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

        // Logic to start spawning
        private bool spawnAllowed;
        private bool firstSpawnDone;

        private List<Recipe> sequencedRecipes = new List<Recipe>();
        private List<RecipeVariation> sequencedRecipeVariations = new List<RecipeVariation>();
        private int sequenceIndex;
        */

        [Header("Orders")]
        public RecipeManager recipeManager;
        public MonoBehaviour[] sequences;
        int minOrdersPerTable = 2;
        int maxOrdersPerTable = 3;

        [Header("Tickets")]
        public TicketGroup ticketGroup;

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

            Debug.Log("About to set table");
            StartCoroutine(SetTableOnDelay(3));
        }

        IEnumerator SetTableOnDelay(float seconds)
        {
            Debug.Log("Setting table");
            yield return new WaitForSeconds(seconds);
            GenerateNewOrder();
        }

        void Update()
        {
            
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

            ticketGroup.SetTickets(recipes);
        }

    }
}