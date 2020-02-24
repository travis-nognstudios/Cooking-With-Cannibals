using UnityEngine;
using System.Collections;

namespace Serving
{ 
    public class ColdShoulderTicket : OrderTicket
    {
        protected override void SetAmounts()
        {
            int shoulderAmount = recipe.mainIngredientAmount;
            UILines[0].amount = shoulderAmount;
        }
    }
}