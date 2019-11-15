using UnityEngine;
using System.Collections;
using Recipes;
using UnityEngine.SceneManagement;

namespace Recipes
{
    public class OrderSpawner : MonoBehaviour
    {
        #region Variables
        [SerializeField, Tooltip("Time between order spawns")]
        private float timeBetweenOrders;
        [SerializeField, Tooltip ("Order ticket game objects")]
        private GameObject[] orderTickets;

        [SerializeField]
        private RecipeManager recipeManager;
        private float orderTime;
        private int i;
        private bool canSpawn;  
        private string lvl1Name;
        private string lvl2Name;
        private string lvl3Name;

        [SerializeField]
        private GameObject spawnPoint;
        #endregion

        void Start()
        {
            orderTime = 0;
            canSpawn = true;
            lvl1Name = "Week 8";
            lvl2Name = "Lvl2";
            lvl3Name = "Lvl3";
            Time.timeScale = 1.0f;
        }


        void Update()
        {
            orderTime = Time.deltaTime + timeBetweenOrders;

            i = Random.Range(0, recipeManager.recipes.Length);
            if (SceneManager.GetActiveScene().name == lvl1Name && canSpawn == true)
            {
                Debug.Log("cansapwn");
                StartCoroutine(SpawnOrder(i));
                
            }

            if (SceneManager.GetActiveScene().name == lvl2Name)
            {
                StartCoroutine(SpawnOrder(i));
            }

            if (SceneManager.GetActiveScene().name == lvl3Name)
            {
                StartCoroutine(SpawnOrder(i));
            }
        }


        IEnumerator SpawnOrder(int i)
        {
            canSpawn = false;
            yield return new WaitForSeconds(timeBetweenOrders);
            Debug.Log("Spawn Ticket");
            Instantiate(orderTickets[i], spawnPoint.transform.position, spawnPoint.transform.rotation);
            canSpawn = true;


        }
    }
}
