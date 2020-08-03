using UnityEngine;
using System.Collections;

namespace Serving
{ 
    public class SisKabobTicket: OrderTicket
    {
        protected override void SetAmounts()
        {
            int elbowAmount = recipe.mainIngredientAmount;
            UILines[0].amount = elbowAmount;

            int lettuceAmount = recipe.toppingAmount[0];
            int eyeAmount = recipe.toppingAmount[1];

            UILines[1].amount = lettuceAmount;
            UILines[2].amount = eyeAmount;
        }
    }
}