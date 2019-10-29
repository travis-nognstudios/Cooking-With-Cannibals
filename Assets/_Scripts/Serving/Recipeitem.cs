using System;
using Cooking;

namespace Serving
{
    [System.Serializable]
    public struct Recipeitem
    {
        public Cooking.Ingredient ingredient;
        public Cooking.Cooktype cooktype;
        public Cooking.Cookstate cookstate;
    }
}