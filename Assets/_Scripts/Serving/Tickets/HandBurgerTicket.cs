using UnityEngine;
using System.Collections;

namespace Serving
{ 
    public class HandBurgerTicket: OrderTicket
    {
        public override void SetAmounts()
        {
            int handAmount = recipe.mainIngredientAmount;
            UILines[0].amount = handAmount;

            int bunAmount = recipe.toppingAmount[0];
            int tomatoAmount = recipe.toppingAmount[1];
            int cheeseAmount = recipe.toppingAmount[2];
            int lettuceAmount = recipe.toppingAmount[3];

            UILines[1].amount = cheeseAmount;
            UILines[2].amount = tomatoAmount;
            UILines[3].amount = lettuceAmount;
            UILines[4].amount = bunAmount;
        }
    }
}