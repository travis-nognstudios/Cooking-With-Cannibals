using System;
using Cooking;

namespace Recipes
{
    [System.Serializable]
    public struct RecipeItem
    {
        public Cooking.Ingredient ingredient;
        public Cooking.CookType cookType;
        public Cooking.CookState cookState;
    }
}