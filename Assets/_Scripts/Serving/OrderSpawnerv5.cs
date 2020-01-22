using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using AI;

namespace Serving
{
    public class OrderSpawnerv5 : MonoBehaviour
    {
        #region Variables

        public GameObject[] ticketSpawnPoints;
        public float ticketSpawnInterval;
        public int numberOfTickets = 10;

        private float timeSinceLastSpawn;
        private int numTicketsSpawned;
        private RecipeManager recipeManager;

        // Separate logic for first spawn
        public float firstSpawnTime = 3f;
        private float firstSpawnTimer = 0f;
        private bool firstSpawnDone;

        private List<TicketPoint> ticketPoints = new List<TicketPoint>();

        // Customer management
        public Customer[] allCustomers;
        private List<Customer> newCustomers = new List<Customer>();
        private List<Customer> waitingCustomers = new List<Customer>();
        private List<Customer> doneCustomers = new List<Customer>();

        #endregion Variables

        void Start()
        {
            recipeManager = GetComponent<RecipeManager>();

            // Register ticketp spawn points
            for (int i=0; i<ticketSpawnPoints.Length; ++i)
            {
                GameObject spawnPoint = ticketSpawnPoints[i];
                TicketPoint ticketPoint = new TicketPoint(spawnPoint);
                ticketPoints.Add(ticketPoint);
            }

            // Register every customer as a new customer
            for (int i=0; i<allCustomers.Length; ++i)
            {
                newCustomers.Add(allCustomers[i]);
            }
        }

        void Update()
        {
            // First spawn
            if (!firstSpawnDone)
            {
                firstSpawnTimer += Time.deltaTime;
                if (firstSpawnTimer > firstSpawnTime)
                {
                    SpawnTicket();
                    firstSpawnDone = true;
                }
            }

            // After first spawn
            else
            {
                if (numTicketsSpawned < numberOfTickets)
                {
                    // Update spawn timer
                    timeSinceLastSpawn += Time.deltaTime;
                    if (timeSinceLastSpawn >= ticketSpawnInterval)
                    {
                        timeSinceLastSpawn = 0;
                        SpawnTicket();
                    }
                }
            }

            // Update ticket ages
            foreach (TicketPoint point in ticketPoints)
            {
                if (point.ContainsTicket())
                {
                    point.AddTicketAge(Time.deltaTime);
                }
            }
            
        }

        private void SpawnTicket()
        {
            // Get a random recipe from the recipe manager
            Recipe recipe = GetRandomRecipe();
            GameObject ticket = recipe.recipeTicket;

            // Attach a customer to order
            Customer customer = newCustomers[newCustomers.Count - 1];
            newCustomers.RemoveAt(newCustomers.Count - 1);
            waitingCustomers.Add(customer);
            customer.GoToOrderingPosition();

            // Get an empty point and create ticket there
            int spawnIndex = GenerateSpawnPointIndex();
            if (spawnIndex != -1)
            {
                ticketPoints[spawnIndex].SetTicket(ticket, recipe);
                ticketPoints[spawnIndex].SetCustomer(customer);
                numTicketsSpawned += 1;
            }

        }

        private Recipe GetRandomRecipe()
        {
            int numRecipes = recipeManager.recipes.Length;
            Recipe pickedRecipe = recipeManager.recipes[Random.Range(0, numRecipes)];

            return pickedRecipe;
        }

        private int GenerateSpawnPointIndex()
        {
            // Get lowest index of empty ticket point
            for (int i=0; i<ticketPoints.Count; ++i)
            {
                TicketPoint point = ticketPoints[i];
                if (!point.ContainsTicket())
                {
                    return i;
                }
            }

            return -1;
        }

        public List<Recipe> GetQueuedRecipes()
        {
            List<Recipe> queuedRecipes = new List<Recipe>();

            // Get all unique recipes from non-empty ticket points
            foreach (TicketPoint point in ticketPoints)
            {
                if (point.ContainsTicket() && !queuedRecipes.Contains(point.recipe))
                {
                    queuedRecipes.Add(point.recipe);
                }
            }

            return queuedRecipes;
        }

        public void DespawnTicket(Recipe finishedrecipe)
        {
            // Find oldest matching recipe
            // Algorithm: (rednur01)
            // Loop through all points
            // Get ticket's age if recipe matches finishedrecipe
            // Otherwise age = 0
            List<float> ticketAges = new List<float>();

            foreach (TicketPoint point in ticketPoints)
            {
                if (point.ContainsTicket() && point.recipe.Equals(finishedrecipe))
                {
                    ticketAges.Add(point.ticketAge);
                }
                else
                {
                    ticketAges.Add(0f);
                }
            }

            // Despawn oldest matching ticket
            int oldestPointIndex = ticketAges.IndexOf(ticketAges.Max());
            Customer customer = ticketPoints[oldestPointIndex].GetCustomer();

            ticketPoints[oldestPointIndex].DestroyTicket();
            customer.GoToEndPosition();
        }
    }
}