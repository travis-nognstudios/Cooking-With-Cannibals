using UnityEngine;
using System.Collections;
using Recipes;
using Serving;
using UnityEngine.SceneManagement;

namespace Recipes
{
    public class OrderSpawnerV2 : MonoBehaviour
    {
        #region Variables
        //[SerializeField, Tooltip("Time between order spawns")]
        //private float timeBetweenOrders;
        [SerializeField, Tooltip ("Order ticket game objects")]
        private GameObject[] orderTickets;

        [SerializeField]
        private RecipeManager recipeManager;
        //private float orderTime;
        public int i;
        private bool canSpawn;  
        private string lvl1Name;
        private string lvl2Name;
        private string lvl3Name;

        [SerializeField]
        public  GameObject container;
        Vector3 spawnPoint;
        Quaternion spawnRotation; 
        Vector3 receiptnOffset = new Vector3(0, 0.1f, 0);
        Vector3 boxOffset = new Vector3(1, 0, 0);
        #endregion

        void Start()
        {
            //orderTime = 0;
            spawnPoint = container.transform.position;
            spawnRotation = container.transform.rotation;
            canSpawn = true;
            lvl1Name = "LevelOne";
            lvl2Name = "Lvl2";
            lvl3Name = "Lvl3";
           // Time.timeScale = 1.0f;
            Instantiate(orderTickets[i], spawnPoint + receiptnOffset, spawnRotation);
            container.transform.parent = orderTickets[i].transform;
        }

        /*
        void Update()
        {
            //orderTime = Time.deltaTime + timeBetweenOrders;

            i = Random.Range(0, recipeManager.recipes.Length);
            if (SceneManager.GetActiveScene().name == lvl1Name && canSpawn == true)
            {
                Debug.Log("cansapwn");
                Instantiate(orderTickets[i], spawnPoint + receiptnOffset, spawnRotation);
                canSpawn = false;
            }

            if (SceneManager.GetActiveScene().name == lvl2Name)
            {
                Instantiate(orderTickets[i], spawnPoint + receiptnOffset, spawnRotation);
            }

            if (SceneManager.GetActiveScene().name == lvl3Name)
            {
                Instantiate(orderTickets[i], spawnPoint + receiptnOffset, spawnRotation);
            }
        }
        */
        //public void OnCollisionrEnter(Collider col)
        private void OnTriggerEnter(Collider col)
        {
            Debug.Log("collided with trigger");
            if (col.gameObject.tag == "destroy")
            {
                Destroy(container.gameObject);
                i = Random.Range(0, recipeManager.recipes.Length);


                Instantiate(container, spawnPoint + boxOffset, spawnRotation);
                Instantiate(orderTickets[i], spawnPoint + receiptnOffset, spawnRotation);

                orderTickets[i].transform.parent = container.transform;
            }   



        }

        /*
        IEnumerator SpawnOrder(int i)
        {
            canSpawn = false;
            yield return new WaitForSeconds(timeBetweenOrders);
            Debug.Log("Spawn Ticket");
            Instantiate(orderTickets[i], spawnPoint.transform.position + receiptnOffset, spawnPoint.transform.rotation);
            //Instantiate(orderTickets[i], spawnPoint.transform.position, spawnPoint.transform.rotation);
            canSpawn = true;


        }
        */
    }
}
