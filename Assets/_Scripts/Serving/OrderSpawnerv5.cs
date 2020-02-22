using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using AI;

namespace Serving
{
    public class OrderSpawnerv5 : MonoBehaviour
    {
        [Header("Tickets")]
        public GameObject[] ticketSpawnPoints;
        public float ticketSpawnInterval;

        [Header("Recipes")]
        public int numberOfTickets = 10;
        public MonoBehaviour[] sequences;

        private float timeSinceLastSpawn;
        private int numTicketsSpawned;
        private int numTicketsCompleted;
        private int numActiveTickets;
        private RecipeManager recipeManager;

        // Logic to start spawning
        private bool spawnAllowed;
        private bool firstSpawnDone;

        private List<TicketPoint> ticketPoints = new List<TicketPoint>();

        [Header("Ordering and Customers")]
        public Customer[] allCustomers;
        public GameObject[] orderingPositions;

        private List<Customer> newCustomers = new List<Customer>();
        private List<Customer> waitingCustomers = new List<Customer>();

        private List<Recipe> sequencedRecipes = new List<Recipe>();
        private List<RecipeVariation> sequencedRecipeVariations = new List<RecipeVariation>();
        private int sequenceIndex;
        
        void Start()
        {
            recipeManager = GetComponent<RecipeManager>();

            // Get sequenced recipes
            foreach (MonoBehaviour sequenceScript in sequences)
            {
                RecipeSequence seq = sequenceScript as RecipeSequence;
                seq.LoadRecipes();
                Recipe[] seqRecipes = seq.GetRecipes();

                foreach (Recipe recipe in seqRecipes)
                {
                    sequencedRecipes.Add(recipe);
                }
            }

            // Get sequenced recipes - Variations
            foreach (MonoBehaviour sequenceScript in sequences)
            {
                RecipeSequence seq = sequenceScript as RecipeSequence;
                seq.LoadRecipes();
                RecipeVariation[] seqRecipeVariations = seq.GetRecipeVariations();

                foreach (RecipeVariation recipeVariation in seqRecipeVariations)
                {
                    sequencedRecipeVariations.Add(recipeVariation);
                }
            }

            
            // DEBUG-START
            Debug.Log($"Total recipes: {sequencedRecipeVariations.Count}");

            foreach (RecipeVariation r in sequencedRecipeVariations)
            {
                int time = System.DateTime.Now.Millisecond;

                Debug.Log($"{time} - Recipe: {r.recipeObject.name}");

                Debug.Log($"{time} - Main: {r.mainIngredient.gameObject} x{r.mainIngredientAmount}");

                for (int i = 0; i < r.toppings.Length; ++i)
                {
                    Debug.Log($"{time} - Topping: {r.toppings[i].name} x{r.toppingAmount[i]}");
                }

                Debug.Log($"{time} - END");
            }
            // DEBUG-END
            
            

            // Register ticket spawn points
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
            if (spawnAllowed)
            {
                // First spawn
                if (!firstSpawnDone)
                {
                    SpawnTicket();
                    firstSpawnDone = true;
                }

                // After first spawn
                else
                {
                    if (numTicketsSpawned < numberOfTickets)
                    {
                        // Update spawn timer
                        timeSinceLastSpawn += Time.deltaTime;
                        if (timeSinceLastSpawn >= ticketSpawnInterval || numActiveTickets == 0)
                        {
                            timeSinceLastSpawn = 0;

                            // Spawn ticket if there is an empty spot
                            if (numActiveTickets < ticketPoints.Count)
                            {
                                SpawnTicket();
                            }
                        }
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

            // Destroy expired tickets
            foreach (TicketPoint point in ticketPoints)
            {
                if (point.HasExpired())
                {
                    DespawnTicketAtPoint(point);
                }
            }
        }

        private void SpawnTicket()
        {
            // Get a random recipe from the recipe manager
            //RecipeVariation recipe = GetSequencedRecipeVariation();
            Recipe recipe = GetSequencedRecipe();

            // Create variation
            RecipeVariation recipeVar = recipe.CreateVariation();
            GameObject ticket = recipeVar.recipeTicket;

            // Attach a customer to order
            Customer customer = newCustomers[newCustomers.Count - 1];
            newCustomers.RemoveAt(newCustomers.Count - 1);
            waitingCustomers.Add(customer);

            // Get an empty point and create ticket there
            // Customer goes to corresponding ordering position
            int spawnIndex = GenerateSpawnPointIndex();
            if (spawnIndex != -1)
            {
                ticketPoints[spawnIndex].SetTicket(ticket, recipeVar);
                ticketPoints[spawnIndex].SetCustomer(customer);
                customer.GoToOrderingPosition(orderingPositions[spawnIndex]);

                numTicketsSpawned += 1;
                numActiveTickets += 1;
            }

        }

        private Recipe GetSequencedRecipe()
        {
            Recipe r = sequencedRecipes[sequenceIndex];
            sequenceIndex++;
            return r;
        }

        private RecipeVariation GetSequencedRecipeVariation()
        {
            RecipeVariation r = sequencedRecipeVariations[sequenceIndex];
            sequenceIndex++;
            return r;
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

        public List<RecipeVariation> GetQueuedRecipes()
        {
            List<RecipeVariation> queuedRecipes = new List<RecipeVariation>();

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

        public void DespawnTicket(RecipeVariation finishedrecipe)
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
            waitingCustomers.Remove(customer);

            numActiveTickets -= 1;
            numTicketsCompleted += 1;
        }

        public void DespawnTicketAtPoint(TicketPoint point)
        {
            Customer customer = point.GetCustomer();
            customer.GoToEndPosition();
            waitingCustomers.Remove(customer);

            point.DestroyTicket();
            numActiveTickets -= 1;
            numTicketsCompleted += 1;
        }

        public int GetNumTicketsCompleted()
        {
            return numTicketsCompleted;
        }

        public void StartSpawning()
        {
            spawnAllowed = true;
        }

        public void StopSpawning()
        {
            spawnAllowed = false;
        }

        public void RemoveAllTickets()
        {
            foreach (TicketPoint point in ticketPoints)
            {
                Customer customer = point.GetCustomer();

                if (customer != null)
                {
                    customer.GoToEndPosition();
                    waitingCustomers.Remove(customer);
                }

                point.DestroyTicket();
            }
        }
    }
}