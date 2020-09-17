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

        private GameObject ticketObject;
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
            // Get order ticket
            currentOrderTicket = recipeVar.recipeTicket.GetComponent<OrderTicket>();
            ticketObject = currentOrderTicket.gameObject;

            // Instantiate
            Vector3 spawnOffset = new Vector3(0, -0.1f, 0);
            Vector3 position = spawnPoint.position + spawnOffset;
            Quaternion rotation = ticketObject.transform.rotation;

            createdTicket = Instantiate(ticketObject, position, rotation);
            createdTicket.GetComponent<SpringJoint>().connectedBody = spawnPoint.GetComponent<Rigidbody>();

            OrderTicket ticketSettings = createdTicket.GetComponent<OrderTicket>();
            if (isVIP) ticketSettings.SetAsVIP();
            ticketSettings.Initialize();

            containsTicket = true;
            recipe = ticketSettings.recipe;

            // Set timer
            ticketFullTime = ticketSettings.GetRecipeTime();
            ticketClock.StartTimer();
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
