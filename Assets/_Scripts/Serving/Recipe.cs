using System;
using UnityEngine;
using Cooking;

namespace Serving
{
    [System.Serializable]
    public struct Recipe
    {
        public GameObject recipeObject;
        public GameObject recipeTicket;
        public float serveTime;
        public CookableIngredient mainIngredient;
        public GameObject[] toppings;
    }
}