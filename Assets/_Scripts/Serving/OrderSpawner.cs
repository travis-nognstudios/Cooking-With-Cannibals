using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RecipeManager;

namespace RecipeManager
{
    public class OrderSpawner : MonoBehaviour
    {
        //array of recipes
        public Recipes[] recipes;

        //index of array
        private int i;

        // Start is called before the first frame update
        void Start()
        {
            recipes[] = new RecipeManager[]; 
            
        }

        // Update is called once per frame
        void Update()
        {
            i = Random.Range(0, recipes.Length);
            if (recipes[i] == )
            {

            }
        }

        void SpawnOrder()
        {
            recipes[i].
        }
    }
}
