using UnityEngine;
using UnityEngine.UI;

namespace Cooking
{
    public class CookUIv2 : MonoBehaviour
    {
        [Header("Background")]
        public Image cookAreaBg;
        public Image paddingAreaBg;
        public float maxForBg = 0.65f;

        [Header("Times")]
        public float cookTime;
        public float overcookTime;
        private float totalTime;

        [Header("Indicator")]
        public Image fillIndicator;
        public float minFill = 0.32f;

        public void SetBackground(float cookTime, float overcookTime)
        {
            this.cookTime = cookTime;
            this.overcookTime = overcookTime;
            this.totalTime = overcookTime;

            SetCookBg();
        }

        /* Bg area filling algorithm
         * Only the "bar" portion of the thermometer counts for 0-100%
         * But the image also includes the "bulb"
         * CookBg and PaddingBg are laid on top, filled right-to-left
         * Right-to-left means imagefill has to be calculated "backwards"
         * 
         * maxForBg:
         *      the imagefill amount at which the Bg fills the "bar"
         *      portion of the thermometer completely. For calculation
         *      purposes, this is as far as the imagefill can go
         */
        void SetBg(Image bg, float time)
        {
            float percentage = time / totalTime;
            float rightToLeftPercentage = 1 - percentage;
            float scaledPercentage = rightToLeftPercentage * maxForBg;
            bg.fillAmount = scaledPercentage;
        }

        void SetCookBg()
        {
            SetBg(cookAreaBg, cookTime);
        }

        /* Fill Indicator filling algorithm
         *      - Get the cook percentage
         *      - Find the indicator's fill "scale"
         *        (time 0 is > 0 imagefill because of the "bulb" part of the image)
         *      - Scale the cook percentage by the fill scale
         *      - Add the minFill for the bulb
         */
        public void UpdateFill(float totalTime, float currentTime)
        {
            float cookPercentage = currentTime / totalTime;
            float fillScale = 1 - minFill;
            float actualFill = cookPercentage * fillScale + minFill;

            fillIndicator.fillAmount = actualFill;
        }
    }
}