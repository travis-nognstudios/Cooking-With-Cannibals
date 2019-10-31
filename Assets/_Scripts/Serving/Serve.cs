using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cooking;

namespace Serving
{
    public class Serve : MonoBehaviour
    {

        private Recipe[] recipes;
        private List<Cookable> meals;

        private int mistakes;
        private int stars;

        // Use this for initialization
        void Start()
        {
            recipes = GetComponents<Recipe>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void MatchMealsWithRecipes()
        {

        }
    }
}