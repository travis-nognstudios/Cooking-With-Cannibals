using UnityEngine;
using System.Collections;

namespace Serving
{
    public class OrderManagerBar : MonoBehaviour
    {
        [Header("Service")]
        public bool isServiceActive;

        [Header("Orders")]
        public int numOrdersTotal;
        public float orderFrequency;
        private float numOrdersMade;
        private float timeUntilNextOrder;
        private float numOrdersCompletedOrMissed;

        [Header("Recipes")]
        public RecipeManagerBar recipeManager;

        [Header("Customers")]
        public BarCustomer[] customers;

        void Update()
        {
            if (numOrdersCompletedOrMissed >= numOrdersTotal)
            {
                isServiceActive = false;
            }

            if (isServiceActive)
            {
                timeUntilNextOrder -= Time.deltaTime;

                if (timeUntilNextOrder <= 0 && numOrdersMade < numOrdersTotal)
                {
                    if (!AllCustomersWaitingForFood())
                    {
                        MakeNewOrder();
                    }
                }
            }
            else
            {
                // Not in service
                // Either paused or ended or otherwise interrupted
            }
        }

        private void MakeNewOrder()
        {
            BarCustomer customer = PickRandomCustomer();
            RecipeCocktail recipe = PickRandomRecipe();
            customer.MakeOrder(recipe);

            numOrdersMade++;
            timeUntilNextOrder = orderFrequency;
        }

        private RecipeCocktail PickRandomRecipe()
        {
            int randomIndex = Random.Range(0, recipeManager.recipes.Length);
            return recipeManager.recipes[randomIndex];
        }

        private BarCustomer PickRandomCustomer()
        {
            int randomIndex = Random.Range(0, customers.Length);
            BarCustomer customer = customers[randomIndex];

            while (customer.isWaitingForFood)
            {
                randomIndex = Random.Range(0, customers.Length);
                customer = customers[randomIndex];
            }

            return customer;
        }

        private bool AllCustomersWaitingForFood()
        {
            int numCustomersTotal = customers.Length;
            int numCustomersWaiting = 0;

            foreach (BarCustomer customer in customers)
            {
                if (customer.isWaitingForFood) numCustomersWaiting++;
            }

            return numCustomersTotal == numCustomersWaiting;
        }

        public void OnCompletedOrder()
        {
            numOrdersCompletedOrMissed++;
        }

        public void OnMissedOrder()
        {
            numOrdersCompletedOrMissed++;
        }
    }
}