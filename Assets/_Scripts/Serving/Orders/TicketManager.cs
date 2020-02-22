using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Serving
{
    public class TicketManager : MonoBehaviour
    {
        [Header("Tickets")]
        public TicketPointv2[] ticketPoints;

        [Header("Counters")]
        public int numTicketsSpawned;
        public int numTicketsCompleted;
        public int numTicketsMissed;

        private int numActiveTickets;

        void Start()
        {

        }

        void Update()
        {
            // Clean up unserved orders
            for (int i=0; i<ticketPoints.Length; ++i)
            {
                TicketPointv2 ticketPoint = ticketPoints[i];

                if (ticketPoint.ContainsTicket() && ticketPoint.HasExpired())
                {
                    RemoveTicket(i);
                    numTicketsMissed++;
                }
            }
        }

        public void AddTicket(RecipeVariation recipeVar, int lane)
        {
            if (ticketPoints[lane].ContainsTicket() == false)
            {
                ticketPoints[lane].SetTicket(recipeVar);
                numActiveTickets++;
                numTicketsSpawned++;
            }
        }

        public void AddTicket(RecipeVariation recipeVar)
        {
            int lane = FindOpenLane();
            AddTicket(recipeVar, lane);
        }

        public void RemoveTicket(int lane)
        {
            if (ticketPoints[lane].ContainsTicket())
            {
                ticketPoints[lane].DestroyTicket();
                numActiveTickets--;
                numTicketsCompleted++;
            }
        }

        public void RemoveTicket(RecipeVariation recipeVar)
        {
            int oldestMatching = IndexOfOldestMatchingRecipe(recipeVar);
            RemoveTicket(oldestMatching);
        }

        public void RemoveAllTickets()
        {
            for (int i=0; i<ticketPoints.Length; ++i)
            {
                if (ticketPoints[i].ContainsTicket())
                {
                    RemoveTicket(i);
                }
            }
        }

        public bool HasRoomForNewTicket()
        {
            return numActiveTickets < 3;
        }

        public bool HasNoTickets()
        {
            return numActiveTickets == 0;
        }

        private int IndexOfOldestMatchingRecipe(RecipeVariation recipeVar)
        {
            List<float> agesOfMatchingTickets = new List<float>();
            foreach (TicketPointv2 ticket in ticketPoints)
            {
                float ticketAge = ticket.ticketAge;
                RecipeVariation ticketRecipe = ticket.recipe;
                bool matchingRecipe = ticketRecipe.Equals(recipeVar);

                if (matchingRecipe)
                {
                    agesOfMatchingTickets.Add(ticketAge);
                }
                else
                {
                    // For non-matching recipes, set age to 0
                    agesOfMatchingTickets.Add(0f);
                }
            }


            int oldest = 0;
            for (int i=1; i<agesOfMatchingTickets.Count; ++i)
            {
                if (agesOfMatchingTickets[oldest] > agesOfMatchingTickets[i])
                {
                    oldest = i;
                }
            }

            return oldest;
        }

        private int FindOpenLane()
        {
            int leftMostOpen = 0;

            for (int i=0; i<ticketPoints.Length; ++i)
            {
                if (ticketPoints[i].ContainsTicket() == false)
                {
                    leftMostOpen = i;
                    break;
                }
            }

            return leftMostOpen;
        }

        public List<RecipeVariation> GetActiveTicketRecipes()
        {
            List<RecipeVariation> recipes = new List<RecipeVariation>();
            foreach (TicketPointv2 ticketPoint in ticketPoints)
            {
                RecipeVariation recipeVar = ticketPoint.recipe;
                recipes.Add(recipeVar);
            }

            return recipes;
        }
    }
}