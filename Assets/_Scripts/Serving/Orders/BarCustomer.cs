using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using Utility;

namespace Serving
{
    public class BarCustomer : MonoBehaviour
    {
        [Header("Ticket")]
        public OrderTicketBar orderTicket;
        public Transform orderPoint;
        private GameObject ticketObject;

        [Header("Order")]
        public bool isWaitingForFood;
        public RecipeCocktail recipe;
        public float timeLeft;
        public UnityEvent orderMissed = new UnityEvent();
        public UnityEventInt orderCompleted = new UnityEventInt();

        [Header("Drink Check")]
        public Coaster coaster;

        void Update()
        {
            if (isWaitingForFood)
            {
                timeLeft -= Time.deltaTime;
                UpdateTicketTimer();

                if (timeLeft <= 0)
                {
                    CancelOrder();
                }
            }

        }

        public void MakeOrder(RecipeCocktail recipe)
        {
            this.recipe = recipe;
            timeLeft = recipe.serveTime;

            SetCoaster();
            SetTicket(recipe.orderTicket);

            isWaitingForFood = true;
        }

        public void CancelOrder()
        {
            ClearCoaster();
            RemoveTicket();

            recipe = null;
            timeLeft = 0;
            orderMissed.Invoke();

            isWaitingForFood = false;
        }

        public void CompleteOrder(int qualityPoints)
        {
            ClearCoaster();
            RemoveTicket();

            recipe = null;
            timeLeft = 0;
            orderCompleted.Invoke(qualityPoints);

            isWaitingForFood = false;
        }

        void SetTicket(OrderTicketBar ticketPrefab)
        {
            ticketObject = Instantiate(ticketPrefab.gameObject, orderPoint);

            OrderTicketBar ticket = ticketObject.GetComponent<OrderTicketBar>();
            orderTicket = ticket;

            orderTicket.ticketClock.StartTimer(isVIP: false);
        }

        void RemoveTicket()
        {
            orderTicket.ticketClock.EndTimer();
            Destroy(ticketObject);

            ticketObject = null;
            orderTicket = null;
        }

        void SetCoaster()
        {
            coaster.customerIsWaiting = true;
            coaster.recipe = recipe;
        }

        void ClearCoaster()
        {
            coaster.customerIsWaiting = false;
            coaster.recipe = null;
        }

        void UpdateTicketTimer()
        {
            orderTicket.ticketClock.UpdateTimer(recipe.serveTime, timeLeft);
        }
    }
}