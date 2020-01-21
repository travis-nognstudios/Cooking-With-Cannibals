using System;
using UnityEngine;
using Cooking;
using System.Collections.Generic;

namespace Serving
{
    public class RecipeRating
    {
        private List<GameObject> itemsInBox;
        private List<string> itemsInBoxNames;
        private Recipe recipe;

        private bool isValidRecipe;
        private int numMistakes = 0;
        private int tipAmount;

        public RecipeRating(List<GameObject> itemsInBox, Recipe recipe)
        {
            this.itemsInBox = itemsInBox;
            this.itemsInBoxNames = GetInBoxNames();
            this.recipe = recipe;

            CheckValidity();
        }

        // Has main ingredient = valid recipe
        public void CheckValidity()
        {
            string mainIngredientShouldBe = recipe.mainIngredient.gameObject.name;

            if (ListContainsName(itemsInBoxNames, mainIngredientShouldBe))
            {
                isValidRecipe = true;
            }
        }

        public void FindMistakes()
        {
            string mainIngredientShouldHave = recipe.mainIngredient.gameObject.name;

            CookMechanic[] cookStepsShouldBe = recipe.mainIngredient.steps;
            GameObject[] toppingsShouldHave = recipe.toppings;

            // Get topping names
            List<string> toppingsShouldHaveNames = new List<string>();
            foreach (GameObject topping in toppingsShouldHave)
            {
                toppingsShouldHaveNames.Add(topping.name);
            }

            // Get main ingredient and its cooked steps
            GameObject mainIngredientIs = new GameObject();
            List<CookMechanic> cookStepsAre;

            for (int i=0; i<itemsInBox.Count; ++i)
            {
                GameObject item = itemsInBox[i];
                if (NameEquals(item.name, mainIngredientShouldHave))
                {
                    mainIngredientIs = item;
                    break;
                }
            }

            cookStepsAre = mainIngredientIs.GetComponent<Cookablev2>().GetSteps();


            // COUNT MISTAKES
            // Each missing topping = 1 mistake
            foreach (string toppingName in toppingsShouldHaveNames)
            {
                if (!ListContainsName(itemsInBoxNames, toppingName))
                {
                    numMistakes++;
                    Debug.Log("Mistake: Missing Topping - " + toppingName);
                }
            }

            // Each extra topping/ingredient = 1 mistake
            foreach (string itemInBox in itemsInBoxNames)
            {
                if (!NameEquals(itemInBox, mainIngredientShouldHave) && !ListContainsName(toppingsShouldHaveNames, itemInBox))
                {
                    numMistakes++;
                    Debug.Log("Mistake: Extra topping - " + itemInBox);
                }
            }

            // Main ingredient cooked wrong = 1 mistake
            // Wrong number of steps/wrong order
            if (cookStepsShouldBe.Length != cookStepsAre.Count)
            {
                numMistakes++;
                Debug.Log("Mistake: wrong number of cook steps - should be " + cookStepsShouldBe.Length + " but is " + cookStepsAre.Count);
            }
            else if (cookStepsShouldBe.Length > 0)
            {
                int numSteps = cookStepsShouldBe.Length;
                for (int i=0; i<numSteps; ++i)
                {
                    if (!cookStepsShouldBe[i].Equals(cookStepsAre[i]))
                    {
                        numMistakes++;
                        Debug.Log("Mistake: Cooked in wrong order");
                        break;
                    }
                }
            }

            CalculateTipAmount();
        }

        private void CalculateTipAmount()
        {
            switch (numMistakes)
            {
                case 0:
                    tipAmount = 3;
                    break;
                case 1:
                    tipAmount = 2;
                    break;
                default:
                    tipAmount = 1;
                    break;
            }
        }

        private List<string> GetInBoxNames()
        {
            List<string> inBoxNames = new List<string>();
            foreach (GameObject item in itemsInBox)
            {
                inBoxNames.Add(item.name);
            }

            return inBoxNames;
        }

        private bool ListContainsName(List<string> list, string name)
        {
            foreach (string listItemName in list)
            {
                // Check if one string is a substring of the other
                // This allows for copies of objects to have the number
                // at the end and still be recognized
                if (name.Contains(listItemName) || listItemName.Contains(name))
                {
                    return true;
                }
            }

            return false;
        }

        private bool NameEquals(string n1, string n2)
        {
            return n1.Contains(n2) || n2.Contains(n1);
        }
    
        public bool GetIsValidRecipe()
        {
            return this.isValidRecipe;
        }

        public int GetTipAmount()
        {
            return this.tipAmount;
        }
    }
}