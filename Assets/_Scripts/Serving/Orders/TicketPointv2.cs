using System;
using UnityEngine;
using AI;

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

        private Customer customer;


        void Start()
        {
            
        }

        void Update()
        {
            if (containsTicket)
            {
                ticketAge += Time.deltaTime;
                ticketClock.UpdateTimer(ticketFullTime, ticketFullTime - ticketAge);
            }
        }

        public void SetTicket(RecipeVariation recipeVar)
        {
            // Get recipe ticket
            currentOrderTicket = recipeVar.recipeTicket.GetComponent<OrderTicket>();
            currentOrderTicket.SetUI();
            ticketObject = currentOrderTicket.gameObject;

            // Instantiate ticket object
            // Attach to spawn point with spring joint
            Vector3 spawnOffset = new Vector3(0, -0.1f, 0);
            Vector3 position = spawnPoint.position + spawnOffset;
            Quaternion rotation = ticketObject.transform.rotation;

            createdTicket = Instantiate(ticketObject, position, rotation);
            createdTicket.GetComponent<SpringJoint>().connectedBody = spawnPoint.GetComponent<Rigidbody>();

            containsTicket = true;

            // Set timer
            ticketFullTime = recipeVar.serveTime;
            ticketClock.StartTimer();
        }

        public void DestroyTicket()
        {
            if (containsTicket)
            {
                Destroy(createdTicket);
                containsTicket = false;

                currentOrderTicket = null;
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
    }
}
