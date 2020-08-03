using UnityEngine;
using System.Collections;

namespace Serving
{ 
    public abstract class OrderTicket : MonoBehaviour
    {
        public TicketUILine[] UILines;

        public RecipeVariation recipe;

        protected abstract void SetAmounts();

        public void SetUI()
        {
            SetAmounts();

            foreach (TicketUILine ui in UILines)
            {
                ui.SetUI();
            }
        }

    }
}