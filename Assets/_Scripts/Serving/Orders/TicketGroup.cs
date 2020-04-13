using System;
using UnityEngine;
using AI;
using LevelManagement;
using System.Collections.Generic;

namespace Serving
{
    public class TicketGroup : MonoBehaviour
    {
        [Header("Tickets")]
        public TicketPointv2[] ticketPoints;

        [Header("Timing")]
        public float tableAge;
        public TicketClock tableClock;
        private float tableFullTime;

        void Start()
        {
            
        }

        void Update()
        {
            if (ContainsTickets())
            {
                tableAge += PauseTimer.DeltaTime();
                // tableClock.UpdateTimer(tableFullTime, tableFullTime - tableAge);
            }
        }

        public void SetTickets(List<RecipeVariation> recipes)
        {
            int numRecipes = recipes.Count;

            for (int i=0; i<numRecipes; ++i)
            {
                ticketPoints[i].SetTicket(recipes[i]);
            }
        }

        public void ClearTickets()
        {
            foreach (TicketPointv2 ticketPoint in ticketPoints)
            {
                if (ticketPoint.ContainsTicket())
                {
                    ticketPoint.DestroyTicket();
                }
            }
        }

        public bool ContainsTickets()
        {
            bool contains = false;

            foreach (TicketPointv2 ticketPoint in ticketPoints)
            {
                contains = contains || ticketPoint.ContainsTicket();
            }

            return contains;
        }

        public bool HasExpired()
        {
            return tableAge > tableFullTime;
        }
        
        public RecipeGroup GetRecipeGroup()
        {
            RecipeGroup recipeGroup = new RecipeGroup();
            List<GameObject> recipeMeals = new List<GameObject>();
     
            foreach (TicketPointv2 ticketPoint in ticketPoints)
            {
                if (ticketPoint.ContainsTicket())
                {
                    recipeMeals.Add(ticketPoint.recipe.baseRecipe.recipeObject);
                }
            }

            recipeGroup.recipes = recipeMeals.ToArray();
            return recipeGroup;
        }
    }
    
}
