using UnityEngine;
using System.Collections;

namespace Serving
{ 
    public class LastBreathTicket : OrderTicket
    {
        protected override void SetAmounts()
        {
            int lungAmount = recipe.mainIngredientAmount;
            UILines[0].amount = lungAmount;
        }
    }
}