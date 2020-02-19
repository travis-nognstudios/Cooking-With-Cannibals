using UnityEngine;
using System.Collections;

namespace Serving
{ 
    public abstract class OrderTicket : MonoBehaviour
    {
        public TicketUILine[] UILines;
        public RecipeVariation recipe;

        public abstract void SetAmounts();

        public void SetUI()
        {
            foreach (TicketUILine ui in UILines)
            {
                ui.SetUI();
            }
        }
    }
}