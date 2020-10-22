using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using Liquor;
using Utility;

namespace Serving
{
    public class Coaster : MonoBehaviour
    {
        [Header("Recipe")]
        public RecipeCocktail recipe;
        public bool customerIsWaiting;

        [Header("Cocktail Check")]
        public UnityEventInt orderCompleted = new UnityEventInt();

        readonly int FoodLayer = 13;
        private bool isChecking;

        private void OnTriggerEnter(Collider other)
        {
            if (customerIsWaiting && !isChecking && other.gameObject.layer == FoodLayer)
            {
                StartCoroutine(ConsumeDrink(other));
            }
        }

        IEnumerator ConsumeDrink(Collider servedCocktail)
        {
            isChecking = true;
            yield return new WaitForSeconds(1);

            Glass cocktailGlass = servedCocktail.gameObject.GetComponent<Glass>();
            bool matches = RecipeMatchesServedCocktail(cocktailGlass);
            int qualityPoints = matches ? 1 : 0;
            orderCompleted.Invoke(qualityPoints);

            Destroy(servedCocktail.gameObject);
            isChecking = false;
        }

        bool RecipeMatchesServedCocktail(Glass cocktailGlass)
        {
            List<string> servedIngredients = cocktailGlass.contains;
            string[] shouldHave = recipe.ingredientNames;

            int numMatching = 0;

            foreach (string ingredient in shouldHave)
            {
                if (servedIngredients.Contains(ingredient))
                    numMatching++;
            }

            return numMatching == shouldHave.Length;
        }
    }
}