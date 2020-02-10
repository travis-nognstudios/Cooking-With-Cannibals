using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cooking
{
    public class CookUI : MonoBehaviour
    {
        public Image fill;
        private float minimumFill = 0.2f;

        // Start is called before the first frame update
        void Start()
        {
            fill.fillAmount = minimumFill;
        }

        public void UpdateFill(float totalTime, float currentTime)
        {
            // Find the percentage amount to fill considering minimum fill
            // Example: if minimum fill is 0.2, then only 80% is left
            // Scale value to 80% (multiply by 1 - 0.2 = 0.8)
            // Then add minimum fill
            float percentage = currentTime / totalTime;
            float offset = 1 - minimumFill;
            float offsetFill = (percentage * offset) + minimumFill;

            fill.fillAmount = offsetFill;
        }
    }
}