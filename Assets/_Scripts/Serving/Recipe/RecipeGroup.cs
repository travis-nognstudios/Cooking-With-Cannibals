using System;
using UnityEngine;
using Cooking;

namespace Serving
{
    [System.Serializable]
    public class RecipeGroup
    {
        public GameObject[] recipes;

        public bool Equals(RecipeGroup other)
        {
            // Match all recipeObject names
            int myLength = recipes.Length;
            int otherLength = other.recipes.Length;

            bool matches = myLength == otherLength;

            if (!matches)
            {
                return false;
            }

            for (int i=0; i<myLength; ++i)
            {
                String recipeName = recipes[i].name;
                bool contains = Array.Exists(other.recipes, element => element.name.Equals(recipeName));
                matches = matches && contains;
            }

            return matches;
        }
    }
}