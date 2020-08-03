using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cooking
{
    public class CookUIv2 : MonoBehaviour
    {
        [Header("Background")]
        public Image cookArea;
        public Image overcookArea;

        [Header("Times")]
        public float cookTime;
        public float overcookTime;
        public float paddingTime = 3f;
        private float totalTime;

        [Header("Indicator")]
        public Image currentStateIndicator;
        public float minimumFillCurrentState = 0.15f;
        public float maximumFillCurrentState = 0.15f;

        private float maximumFill = 0.78f;


        public void SetBackground(float cookTime, float overcookTime)
        {
            this.cookTime = cookTime;
            this.overcookTime = overcookTime;
            totalTime = overcookTime + paddingTime;

            SetCookFill();
            SetOvercookFill();
        }

        void SetCookFill()
        {
            float fillPercentage = cookTime / totalTime;
            float fillArea = maximumFill - (maximumFill * fillPercentage);
            cookArea.fillAmount = fillArea;
        }

        void SetOvercookFill()
        {
            float fillPercentage = overcookTime / totalTime;
            float fillArea = maximumFill - (maximumFill * fillPercentage);
            overcookArea.fillAmount = fillArea;
        }

        public void UpdateFill(float totalTime, float currentTime)
        {
            // Find the percentage amount to fill considering minimum fill
            // Example: if minimum fill is 0.2, then only 80% is left
            // Scale value to 80% (multiply by 1 - 0.2 = 0.8)
            // Then add minimum fill
            float percentage = currentTime / (totalTime + paddingTime);
            float offset = 1 - minimumFillCurrentState;
            float offsetFill = (percentage * offset) + minimumFillCurrentState;

            currentStateIndicator.fillAmount = offsetFill;
        }
    }
}