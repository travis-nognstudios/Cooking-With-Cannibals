using UnityEngine;
using System.Collections;
using Recipes;
using UnityEngine.SceneManagement;

namespace Recipes
{
    public class OrderSpawnerV3 : MonoBehaviour
    {
        #region Variables
        [SerializeField, Tooltip("Time between order spawns")]
        public float spawntime = 30.0f;
        //private float timeBetweenOrders;
        [SerializeField, Tooltip("Order ticket game objects")]
        private GameObject[] orderTickets;

        [SerializeField]
        private RecipeManager recipeManager;
        private float orderTime1;
        private float orderTime2;
        private float orderTime3;

        private int i;
        private float startTime;

        private bool canSpawn1;
        private bool canSpawn2;
        private bool canSpawn3;
      

        [SerializeField]
        public GameObject spawnPoint1;
        public GameObject spawnPoint2;
        public GameObject spawnPoint3;

        private GameObject ticketinPoint1;
        private GameObject ticketinPoint2;
        private GameObject ticketinPoint3;
        #endregion

        void Start()
        {
            /* ToDo: Figure out why this breaks the camera!!   
            // Spring joints on tickets
            foreach (GameObject ticket in orderTickets)
            {
                ticket.GetComponent<FixedJoint>().connectedBody = spawnPoint1.GetComponent<Rigidbody>();
            }
            */

            //orderTime = 0;
            i = Random.Range(0, recipeManager.recipes.Length);
            canSpawn1 = false;
            canSpawn2 = true;
            canSpawn3 = true;
            Instantiate(orderTickets[i], spawnPoint1.transform.position, spawnPoint1.transform.rotation);

            //startTime = Time.time;
            //Time.timeScale = 1.0f;
        }
        

        void Update()
        {
            //orderTime = Time.deltaTime + spawntime;

            i = Random.Range(0, recipeManager.recipes.Length);

            if (checkSpawnAvailable(1))
            {
                SpawnOrder(i, spawnPoint1);      //StartCoroutine(SpawnOrder(i, spawnPoint1));
                changeAvailable(1, false);
                ticketinPoint1 = orderTickets[i];
                orderTime1 = Time.time;
            }
            else if (checkSpawnAvailable(2))
            {
                SpawnOrder(i, spawnPoint2);
                changeAvailable(2, false);
                ticketinPoint2 = orderTickets[i];
                orderTime2 = Time.time;
            }
            else if (checkSpawnAvailable(3))
            {
                SpawnOrder(i, spawnPoint3);
                changeAvailable(3, false);
                ticketinPoint3 = orderTickets[i];
                orderTime3 = Time.time;
            }
            else
            {
                Debug.Log("No Place to Spawn");
            }
        }

        void newTicket(int i, GameObject spawnPoint)
        {
            Instantiate(orderTickets[i], spawnPoint.transform.position, spawnPoint.transform.rotation);

        }

        bool checkSpawnAvailable(int x)
        {
            if (x == 1 && canSpawn1 == true)
                return true;
            else if (x == 2 && canSpawn2 == true)
                return true;
            else if (x == 3 && canSpawn3 == true)
                return true;
            else
                return false;
        }

        void changeAvailable(int x, bool tf)
        {
            if (x == 1)
                canSpawn1 = tf;                
            else if (x == 2)
                canSpawn2 = tf;
            else if (x == 3)
                canSpawn3 = tf;
            else
                Debug.Log("No Change Available");
        }

        public string oldestTicket()
        {
            string oldTicket;

            if (orderTime1 < orderTime2 && orderTime1 < orderTime3)
                oldTicket = ticketinPoint1.gameObject.name;
            else if(orderTime2 < orderTime1 && orderTime2 < orderTime3)
                oldTicket = ticketinPoint2.gameObject.name;
            else if(orderTime3 < orderTime1 && orderTime3 < orderTime2)
                oldTicket = ticketinPoint3.gameObject.name;
            else
                oldTicket = "";

            return oldTicket;
        }

        void removeoldestTicket(int i, GameObject ticket)
        {
            if (orderTime1 < orderTime2 && orderTime1 < orderTime3)
            {
                Destroy(ticketinPoint1);
                changeAvailable(1, true);
                orderTime1 = 0;
            }
            else if (orderTime2 < orderTime1 && orderTime2 < orderTime3)
            {
                Destroy(ticketinPoint2);
                changeAvailable(2, true);
                orderTime2 = 0;
            }
            else if (orderTime3 < orderTime1 && orderTime3 < orderTime2)
            {
                Destroy(ticketinPoint3);
                changeAvailable(3, true);
                orderTime3 = 0;
            }
            else
            {
                Debug.Log("No Tickets Available");
            }
        }

        public Recipe getrecipeObject()
        {
            Recipe[] recipes = recipeManager.GetComponent<RecipeManager>().recipes;

            foreach (Recipe recipe in recipes)
            {
                if (recipe.recipeObject.name == oldestTicket())
                {
                    return recipe;
                }
           
            }
            return new Recipe();
        }

        IEnumerator SpawnOrder(int i, GameObject spawn)
        {
            
            yield return new WaitForSeconds(spawntime);
            newTicket(i, spawn);

        }
    }
}
