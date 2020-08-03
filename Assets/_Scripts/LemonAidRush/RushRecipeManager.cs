using System;
using UnityEngine;
using Serving;

namespace LemonAidRush
{
    public class RushRecipeManager : MonoBehaviour
    {
        public Recipe[] recipes;

        void Start()
        {
            
        }

        void Update()
        {

        }

        public Recipe GetRandomRecipe()
        {
            int randomIndex = UnityEngine.Random.Range(0, recipes.Length);
            return recipes[randomIndex];
        }
    }
}
