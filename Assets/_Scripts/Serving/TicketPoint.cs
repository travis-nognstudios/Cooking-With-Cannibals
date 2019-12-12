using System;
using UnityEngine;

namespace Serving
{
    [System.Serializable]
    public class TicketPoint
    {
        public GameObject spawnPoint;
        public GameObject ticketReference;
        public float ticketAge;
        public Recipe recipe;

        public TicketPoint(GameObject spawnPoint)
        {
            this.spawnPoint = spawnPoint;
            this.ticketReference = null;
            this.ticketAge = 0f;
            this.recipe = new Recipe();
        }

        public void SetTicket(GameObject ticketReference, Recipe recipe)
        {
            this.ticketReference = ticketReference;
            this.ticketAge = 0f;
            this.recipe = recipe;

            // Instantiate ticket object
            // Create spring joint to spawn point
            Vector3 ticketOffset = new Vector3(0, -0.1f, 0);
            Vector3 position = spawnPoint.transform.position + ticketOffset;
            Quaternion rotation = ticketReference.transform.rotation;

            GameObject createdTicket = UnityEngine.Object.Instantiate(ticketReference, position, rotation);
            createdTicket.GetComponent<SpringJoint>().connectedBody = spawnPoint.GetComponent<Rigidbody>();
        }

        public void DestroyTicket()
        {
            if (this.ticketReference != null)
            {
                UnityEngine.Object.Destroy(this.ticketReference);

                this.ticketReference = null;
                this.ticketAge = 0f;
                this.recipe = new Recipe();
            }
        }

        public bool ContainsTicket()
        {
            return this.ticketReference != null;
        }

        public void AddTicketAge(float age)
        {
            this.ticketAge += age;
        }

    }
}
