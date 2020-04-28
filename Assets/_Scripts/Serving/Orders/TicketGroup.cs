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
        public float tableFullTime;

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

            SetFullTime();
        }

        public void SetFullTime()
        {
            float fullTime = 0;

            foreach (TicketPointv2 ticketPoint in ticketPoints)
            {
                if (ticketPoint.ContainsTicket())
                {
                    fullTime += ticketPoint.GetFullTime();
                }
            }

            tableFullTime = fullTime;
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

            tableFullTime = 0;
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
                    GameObject recipeObject = ticketPoint.recipe.baseRecipe.recipeObject;
                    recipeMeals.Add(recipeObject);
                }
            }

            recipeGroup.recipes = recipeMeals.ToArray();
            return recipeGroup;
        }
    }
    
}
