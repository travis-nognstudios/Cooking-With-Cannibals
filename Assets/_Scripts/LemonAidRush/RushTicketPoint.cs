using System;
using UnityEngine;
using Serving;

namespace LemonAidRush
{
    public class RushTicketPoint : MonoBehaviour
    {
        [Header("Objects")]
        public Transform spawnPoint;
        public OrderTicket currentOrderTicket;

        private GameObject ticketObject;
        private GameObject createdTicket;
        private bool containsTicket;
        
        [Header("Recipe")]
        public RecipeVariation recipe;
        
        void Start()
        {
            
        }

        void Update()
        {

        }

        public void SetTicket(RecipeVariation recipeVar)
        {
            // Get order ticket
            currentOrderTicket = recipeVar.recipeTicket.GetComponent<OrderTicket>();
            //currentOrderTicket.SetUI();
            ticketObject = currentOrderTicket.gameObject;

            recipe = recipeVar;

            // Instantiate ticket object
            // Attach to spawn point with spring joint
            Vector3 spawnOffset = new Vector3(0, -0.1f, 0);
            Vector3 position = spawnPoint.position + spawnOffset;
            Quaternion rotation = spawnPoint.transform.rotation;

            createdTicket = Instantiate(ticketObject, position, rotation);
            createdTicket.GetComponent<SpringJoint>().connectedBody = spawnPoint.GetComponent<Rigidbody>();
            createdTicket.GetComponent<OrderTicket>().SetUI();

            containsTicket = true;
        }

        public void DestroyTicket()
        {
            if (containsTicket)
            {
                Destroy(createdTicket);
                containsTicket = false;

                currentOrderTicket = null;
                recipe = null;
            }
        }

        public bool ContainsTicket()
        {
            return containsTicket;
        }
    }
}
