using System;
using UnityEngine;
using AI;
using LevelManagement;

namespace Serving
{
    public class TicketPointv2 : MonoBehaviour
    {
        [Header("Objects")]
        public Transform spawnPoint;
        public OrderTicket currentOrderTicket;

        private GameObject createdTicket;
        private bool containsTicket;

        [Header("Timing")]
        public float ticketAge;
        public TicketClock ticketClock;
        private float ticketFullTime;

        [Header("Recipe")]
        public RecipeVariation recipe;
        public int recipeTipAmount;

        private Customer customer;

        void Update()
        {
            if (containsTicket)
            {
                ticketAge += PauseTimer.DeltaTime();
                ticketClock.UpdateTimer(ticketFullTime, ticketFullTime - ticketAge);
            }
        }

        public void SetTicket(RecipeVariation recipeVar, bool isVIP)
        {
            // Get ticket object associated with recipe
            OrderTicket orderTicket = recipeVar.recipeTicket.GetComponent<OrderTicket>();
            GameObject ticketObject = orderTicket.gameObject;

            // Instantiate and attach to window
            Vector3 spawnOffset = new Vector3(0, -0.1f, 0);
            Vector3 position = spawnPoint.position + spawnOffset;
            Quaternion rotation = ticketObject.transform.rotation;

            createdTicket = Instantiate(ticketObject, position, rotation);
            createdTicket.GetComponent<SpringJoint>().connectedBody = spawnPoint.GetComponent<Rigidbody>();

            // Get ticket, set default recipe then configure
            currentOrderTicket = createdTicket.GetComponent<OrderTicket>();
            currentOrderTicket.recipe = recipeVar;

            if (isVIP) currentOrderTicket.SetAsVIP();
            currentOrderTicket.Initialize();

            containsTicket = true;
            recipe = currentOrderTicket.recipe;

            // Set timer
            ticketFullTime = currentOrderTicket.GetRecipeTime();
            ticketClock.StartTimer(isVIP);
        }

        public void DestroyTicket()
        {
            if (containsTicket)
            {
                Destroy(createdTicket);
                containsTicket = false;

                currentOrderTicket = null;
                recipe = null;
                recipeTipAmount = 0;
                ticketAge = 0;
                ticketClock.EndTimer();
            }
        }

        public void SetCustomer(Customer customer)
        {
            this.customer = customer;
        }

        public Customer GetCustomer()
        {
            return customer;
        }

        public bool ContainsTicket()
        {
            return containsTicket;
        }

        public bool HasExpired()
        {
            return ticketAge >= ticketFullTime;
        }

        public float GetFullTime()
        {
            return ticketFullTime;
        }
    }
}
