using System;
using UnityEngine;
using AI;

namespace Serving
{
    [System.Serializable]
    public class TicketPoint
    {
        public GameObject spawnPoint;
        public GameObject ticketReference;
        public float ticketAge;
        public RecipeVariation recipe;
        public TicketClock ticketTiming;

        private Customer customer;
        private float timeUntilExpire;


        public TicketPoint(GameObject spawnPoint)
        {
            this.spawnPoint = spawnPoint;
            this.ticketReference = null;
            this.ticketAge = 0f;
            this.recipe = null;
            this.ticketTiming = null;

            this.customer = null;
            this.timeUntilExpire = 99999f;
        }

        public void SetTicket(GameObject ticketReference, RecipeVariation recipe)
        {
            // Instantiate ticket object
            // Create spring joint to spawn point
            Vector3 ticketOffset = new Vector3(0, -0.1f, 0);
            Vector3 position = spawnPoint.transform.position + ticketOffset;
            Quaternion rotation = ticketReference.transform.rotation;

            GameObject createdTicket = UnityEngine.Object.Instantiate(ticketReference, position, rotation);
            createdTicket.GetComponent<SpringJoint>().connectedBody = spawnPoint.GetComponent<Rigidbody>();

            // Set variation UI
            OrderTicket orderTicket = createdTicket.GetComponent<OrderTicket>();
            //orderTicket.SetAmounts();
            orderTicket.SetUI();

            // Set values
            this.ticketReference = createdTicket;
            this.ticketAge = 0f;
            this.recipe = recipe;
            this.timeUntilExpire = recipe.serveTime;
            this.ticketTiming = spawnPoint.GetComponent<TicketClock>();

            ticketTiming.StartTimer();
        }

        public void SetCustomer(Customer customer)
        {
            this.customer = customer;
        }

        public Customer GetCustomer()
        {
            return this.customer;
        }

        public void DestroyTicket()
        {
            if (this.ticketReference != null)
            {
                UnityEngine.Object.Destroy(this.ticketReference);

                this.ticketReference = null;
                this.ticketAge = 0f;
                this.recipe = null;

                this.ticketTiming.EndTimer();
                this.ticketTiming = null;

                this.customer = null;
                this.timeUntilExpire = 99999f;
            }
        }

        public bool ContainsTicket()
        {
            return this.ticketReference != null;
        }

        public void AddTicketAge(float age)
        {
            this.ticketAge += age;
            this.timeUntilExpire -= age;
            this.ticketTiming.UpdateTimer(timeUntilExpire + ticketAge, timeUntilExpire);
        }

        public bool HasExpired()
        {
            return timeUntilExpire <= 0;
        }
    }
}
