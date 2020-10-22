using UnityEngine;
using System.Collections;

namespace Serving
{
    public class RatingManagerBar : MonoBehaviour
    {
        [Header("Rating Thresholds")]
        public int tipsForA;
        public int tipsForB;
        public int tipsForC;

        [Header("Scene Objects")]
        public TipPosterBar tipPoster;
        public RatingCardBar ratingCard;

        [Header("Service")]
        public OrderManagerBar orderManager;

        void Start()
        {
            SetTipPoster();
        }

        void SetTipPoster()
        {
            tipPoster.tipsForA = tipsForA;
            tipPoster.tipsForB = tipsForB;
            tipPoster.tipsForC = tipsForC;
            tipPoster.SetUI();
        }

        public void ShowRatingCard()
        {
            ratingCard.SetGrade(CalculateLetterGrade());
            ratingCard.SetTime(orderManager.totalServiceTime);
            ratingCard.gameObject.SetActive(true);
        }

        private string CalculateLetterGrade()
        {
            string achievedGrade = "F";

            if (tipPoster.currentTips >= tipsForA)
                achievedGrade = "A";
            else if (tipPoster.currentTips >= tipsForB)
                achievedGrade = "B";
            else if (tipPoster.currentTips >= tipsForC)
                achievedGrade = "C";

            return achievedGrade;
        }
    }
}