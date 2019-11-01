using System;
using Cooking;

namespace Serving
{
    [System.Serializable]
    public struct RecipeItem
    {
        public Cooking.Ingredient ingredient;
        public Cooking.CookType cookType;
        public Cooking.CookState cookState;
    }
}