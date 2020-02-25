using UnityEngine;
using System.Collections;

namespace Serving
{ 
    public class CCCTicket: OrderTicket
    {
        protected override void SetAmounts()
        {
            int heartAmount = recipe.mainIngredientAmount;
            UILines[0].amount = heartAmount;

            int cheeseAmount = recipe.toppingAmount[0];

            UILines[1].amount = cheeseAmount;
        }
    }
}