using System;
using UnityEngine;
using Cooking;

namespace Recipes
{
    [System.Serializable]
    public struct Recipe
    {
        public GameObject recipeObject;
        public GameObject recipeTicket;
        public CookableIngredient mainIngredient;
        public GameObject[] toppings;
    }
}