using System;

namespace Serving
{
    [System.Serializable]
    public class RecipeVariation
    {
        public int mainIngredientAmount;
        public int maxMainIngredientAmount = 2;

        public int[] toppingAmount;
        public int maxToppingAmount = 5;
    }
}