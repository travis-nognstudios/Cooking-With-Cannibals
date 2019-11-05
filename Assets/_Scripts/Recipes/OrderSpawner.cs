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

        private float orderTime;
        private Recipe[] recipes;
        private int i;

        private string lvl1Name;
        private string lvl2Name;
        private string lvl3Name;



        #endregion

        void Start()
        {
            orderTime = 0;
            lvl1Name = "Lvl1";
            lvl2Name = "Lvl2";
            lvl3Name = "Lvl3";
            recipes = new Recipe[recipes.Length];
            Time.timeScale = 1.0f;
        }


        void Update()
        {
            orderTime = Time.deltaTime + timeBetweenOrders;

            i = Random.Range(0, recipes.Length);
            if (SceneManager.GetActiveScene().name == lvl1Name)
            {
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
            yield return new WaitForSeconds(timeBetweenOrders);


        }
    }
}
