using UnityEngine;
using System.Collections;

namespace Serving
{ 
    public abstract class OrderTicket : MonoBehaviour
    {
        public TicketUILine[] UILines;

        public bool isVIP;
        public ParticleSystem vipFX;

        public RecipeVariation recipe;

        protected abstract void SetAmounts();

        public void Initialize()
        {
            if (isVIP) ConfigureVIP();
            //SetUI();
        }

        public void SetUI()
        {
            SetAmounts();

            foreach (TicketUILine ui in UILines)
            {
                ui.SetUI();
            }
        }

        public void SetAsVIP()
        {
            isVIP = true;
        }

        public void ConfigureVIP()
        {
            recipe.serveTime *= 0.5f;
            vipFX.Play();
        }

        public float GetRecipeTime()
        {
            return recipe.serveTime;
        }
    }
}