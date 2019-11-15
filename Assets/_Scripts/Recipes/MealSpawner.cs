using UnityEngine;
using System.Collections;

namespace Recipes
{
    public class MealSpawner : MonoBehaviour
    {
        [SerializeField]
        private RecipeManager recipeManager;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
             
        }
        public void SpawnMeal(int i)
        {
            Debug.Log("Spawn Meal");
            Instantiate(recipeManager.recipes[i].recipeObject,transform.position,transform.rotation);
        }
        
    }
}