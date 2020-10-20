using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Serving
{
    public class TipPosterBar : MonoBehaviour
    {
        [Header("Tips")]
        public int currentTips = 0;
        public int tipsForA = 30;
        public int tipsForB = 20;
        public int tipsForC = 10;

        [Header("UI")]
        public Image beerFill;
        public Text score_Current;
        public Text score_A;
        public Text score_B;
        public Text score_C;

        [Header("Fill Thresholds")]
        public float lowerbound_C = 0.26f;
        public float lowerbound_B = 0.50f;
        public float lowerbound_A = 0.71f;
        public float full         = 0.93f;
        public float currentFill  = 0.26f;

        public void SetUI()
        {
            score_A.text = tipsForA.ToString();
            score_B.text = tipsForB.ToString();
            score_C.text = tipsForC.ToString();
            SetBeerFill();
        }

        public void UpdateUIOnTip()
        {
            score_Current.text = currentTips.ToString();
            SetBeerFill();
        }

        public void SetBeerFill()
        {
            // Fill is calculated separately for each grade bracket
            float currentGradeBracket = tipsForC;
            float previousGradeBracket = 0;
            float minFill = lowerbound_C;
            float maxFill = lowerbound_B;

            if (currentTips > tipsForC && currentTips <= tipsForB)
            {
                currentGradeBracket = tipsForB;
                previousGradeBracket = tipsForC;
                minFill = lowerbound_B;
                maxFill = lowerbound_A;
            }
            else if (currentTips > tipsForB)
            {
                currentGradeBracket = tipsForA;
                previousGradeBracket = tipsForB;
                minFill = lowerbound_A;
                maxFill = full;
            }


            float currentScorePercentage = (((float)currentTips - previousGradeBracket) / ((float)currentGradeBracket - previousGradeBracket ));
            currentScorePercentage = Mathf.Min(currentScorePercentage, 1);

            float fillScale = maxFill - minFill;
            float currentFillRatio = fillScale * currentScorePercentage;

            currentFill = currentFillRatio + minFill;
            beerFill.fillAmount = currentFill;
        }
    }
}