using UnityEngine;
using UnityEngine.Events;
using System.Collections;

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
        public float recipeTotalTime;
        public float timeLeft;
        public UnityEvent orderMissed = new UnityEvent();

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
            recipeTotalTime = recipe.serveTime;
            timeLeft = recipeTotalTime;

            SetTicket(recipe.orderTicket);
            isWaitingForFood = true;
        }

        public void CancelOrder()
        {
            RemoveTicket();
            recipeTotalTime = 0;
            timeLeft = 0;
            orderMissed.Invoke();

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

        void UpdateTicketTimer()
        {
            orderTicket.ticketClock.UpdateTimer(recipeTotalTime, timeLeft);
        }
    }
}