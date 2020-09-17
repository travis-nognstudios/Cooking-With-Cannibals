using UnityEngine;
using System.Collections;

namespace Serving
{ 
    public class OrderTicket : MonoBehaviour
    {
        [Header("VIP")]
        public bool isVIP;
        public ParticleSystem vipFX;
        public static int VIPTipBonus = 2;
        public static float VIPTimeMultiplier = 0.5f;

        [Header("Recipe")]
        public RecipeVariation recipe;

        public void Initialize()
        {
            if (isVIP) ConfigureVIP();
        }

        public void SetAsVIP()
        {
            isVIP = true;
        }

        public void ConfigureVIP()
        {
            recipe.serveTime *= OrderTicket.VIPTimeMultiplier;
            vipFX.Play();
        }

        public float GetRecipeTime()
        {
            return recipe.serveTime;
        }
    }
}