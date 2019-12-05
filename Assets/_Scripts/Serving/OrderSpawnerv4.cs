using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Recipes;

namespace Serving
{
    public class OrderSpawnerv4 : MonoBehaviour
    {
        #region Variables

        public GameObject[] ticketSpawnPoints;
        public float ticketSpawnInterval;

        private float timeSinceLastSpawn;
        private RecipeManager recipeManager;

        // Separate logic for first spawn
        public float firstSpawnTime = 3f;
        private float firstSpawnTimer = 0f;
        private bool firstSpawnDone;

        // Track which points have tickets
        // and how old each ticket is
        // and the corresponding recipe
        private List<bool> spawnPointContainsTicket = new List<bool>();
        private List<float> ticketAges = new List<float>();
        private List<Recipe> spawnedTicketRecipes = new List<Recipe>();
        private List<GameObject> ticketReferences = new List<GameObject>();


        #endregion Variables

        void Start()
        {
            recipeManager = GetComponent<RecipeManager>();

            // Initialize all spawnpoints to be empty,
            // all ticket ages to be 0
            // all corresponding recipes to be empty
            // all ticket references to be empty
            foreach (GameObject point in ticketSpawnPoints)
            {
                spawnPointContainsTicket.Add(false);
                ticketAges.Add(0f);
                spawnedTicketRecipes.Add(new Recipe());
                ticketReferences.Add(new GameObject());
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

            // Update spawn timer
            timeSinceLastSpawn += Time.deltaTime;
            if (timeSinceLastSpawn >= ticketSpawnInterval)
            {
                timeSinceLastSpawn = 0;
                SpawnTicket();
            }

            // Update ticket ages
            for (int i=0; i<ticketSpawnPoints.Length; ++i)
            {
                if (spawnPointContainsTicket[i] == true)
                {
                    ticketAges[i] += Time.deltaTime;
                }
            }
        }

        private void SpawnTicket()
        {
            // Get a random recipe from the recipe manager
            Recipe recipe = GetRandomRecipe();
            GameObject ticket = recipe.recipeTicket;
            Vector3 ticketOffset = new Vector3(0, -0.1f, 0);

            // Find an empty spawn point
            // Generate a ticket there and keep a reference
            // Add spring joint to the spawn point's ridigbody
            int spawnIndex = GenerateSpawnPointIndex();
            if (spawnIndex != -1)
            {
                spawnedTicketRecipes[spawnIndex] = recipe;
                spawnPointContainsTicket[spawnIndex] = true;

                GameObject spawnPoint = ticketSpawnPoints[spawnIndex];
                ticketReferences[spawnIndex] = Instantiate(ticket, spawnPoint.transform.position + ticketOffset, ticket.transform.rotation);
                ticketReferences[spawnIndex].GetComponent<SpringJoint>().connectedBody = spawnPoint.GetComponent<Rigidbody>();
            }
        }

        private Recipe GetRandomRecipe()
        {
            int numRecipes = recipeManager.recipes.Length;
            Recipe pickedRecipe = recipeManager.recipes[Random.Range(0,numRecipes)];

            return pickedRecipe;
        }

        private int GenerateSpawnPointIndex()
        {
            for (int i=0; i<ticketSpawnPoints.Length; ++i)
            {
                if (!spawnPointContainsTicket[i])
                {
                    return i;
                }
            }

            return -1;
        }

        public List<Recipe> GetQueuedRecipes()
        {
            List<Recipe> recipes = new List<Recipe>();

            for (int i=0; i<ticketSpawnPoints.Length; ++i)
            {
                if (spawnPointContainsTicket[i] && !recipes.Contains(spawnedTicketRecipes[i]))
                {
                    recipes.Add(spawnedTicketRecipes[i]);
                }
            }

            return recipes;
        }

        public void DespawnTicket(Recipe finishedrecipe)
        {

            // Get all ticket indices that match the finished recipe
            List<int> ticketIndicesMatchingRecipe = new List<int>();
            for (int i=0; i<ticketSpawnPoints.Length; ++i)
            {
                if (spawnedTicketRecipes[i].recipeObject.gameObject.name == finishedrecipe.recipeObject.gameObject.name)
                {
                    ticketIndicesMatchingRecipe.Add(i);
                }
            }

            // Find oldest matching tickets's index
            int ticketIndex = 0;
            for (int i = 0; i < ticketIndicesMatchingRecipe.Count; ++i)
            {
                if (ticketIndicesMatchingRecipe[i] > ticketIndicesMatchingRecipe[ticketIndex])
                {
                    ticketIndex = i;
                }
            }

            // Despawn ticket
            spawnPointContainsTicket[ticketIndex] = false;
            ticketAges[ticketIndex] = 0f;
            spawnedTicketRecipes[ticketIndex] = new Recipe();
            Destroy(ticketReferences[ticketIndex]);
        }
    }
}