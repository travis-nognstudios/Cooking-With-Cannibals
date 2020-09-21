using System;
using UnityEngine;
using Serving;
using LevelManagement;

namespace LemonAidRush
{
    public class RushOrderSpawner : MonoBehaviour
    {
        [Header("Recipes")]
        public RushRecipeManager recipeManager;

        [Header("Timing")]
        public float totalTime;
        public TicketClock ticketClock;

        [Header("Ticket")]
        public RushTicketPoint ticketPoint;

        [Header("GameLoop")]
        public PauseManagerv2 pauseManager;

        private float timer;
        private bool started;
        private bool ended;

        void Start()
        {
            
        }

        void Update()
        {
            UpdateTimer();
            GenerateOrder();
        }

        private void UpdateTimer()
        {
            if (started && !ended)
            {
                timer += Time.deltaTime;
                ticketClock.UpdateTimer(totalTime, totalTime - timer);

                if (timer >= totalTime)
                {
                    EndRushMode();
                }
            }
        }

        public void StartRushMode()
        {
            started = true;
            ticketClock.StartTimer(false);
        }

        public void EndRushMode()
        {
            ended = true;
            ticketClock.EndTimer();

            if (ticketPoint.ContainsTicket())
            {
                ticketPoint.DestroyTicket();
            }

            ReturnToGame();
        }

        private void ReturnToGame()
        {
            pauseManager.SetUnpause();
            pauseManager.SetLocationToPauseArea();
        }
        
        private RecipeVariation GenerateNextRecipe()
        {
            Recipe recipe = recipeManager.GetRandomRecipe();
            RecipeVariation recipeVar = recipe.CreateVariation();
            return recipeVar;
        }

        private void GenerateOrder()
        {
            if (started && !ended)
            {
                if (!ticketPoint.ContainsTicket())
                {
                    RecipeVariation recipeVar = GenerateNextRecipe();
                    ticketPoint.SetTicket(recipeVar);
                }
            }
        }

        public void MadeRecipe(Recipe recipe)
        {
            // Made matching recipe
            if (ticketPoint.currentOrderTicket.recipe.baseRecipe.Equals(recipe))
            {
                ticketPoint.DestroyTicket();

                // TODO: Update score
            }
        }
    }
}
