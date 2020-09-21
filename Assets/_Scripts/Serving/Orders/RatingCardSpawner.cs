using UnityEngine;
using System.Collections;

namespace Serving
{
    public class RatingCardSpawner : MonoBehaviour
    {

        public enum Rating { A, B, C, F}

        [Header("Rating Cards")]
        public GameObject cardGradeA;
        public GameObject cardGradeB;
        public GameObject cardGradeC;
        public GameObject cardGradeF;

        [Header("Ratings By Tip")]
        public int tipsForA;
        public int tipsForB;
        public int tipsForC;

        [Header("Scene Objects")]
        public TipJar tipjar;
        public GradePoster gradePoster;
        public RatingCardPoint cardPoint;

        [Header("Audio")]
        public AudioSource audioSource;
        public AudioClip tootyHorn;

        // Use this for initialization
        void Start()
        {
            SyncTipJarCapacity();
            SyncGradePoster();
        }
        
        void SyncGradePoster()
        {
            gradePoster.SetScores(tipsForA, tipsForB, tipsForC);
            gradePoster.SetPosterText();
        }

        void SyncTipJarCapacity()
        {
            tipjar.capacity = tipsForA;
        }

        public void SpawnCard()
        {
            audioSource.clip = tootyHorn;
            audioSource.Play();

            GameObject selectedCard;

            if (RatedA())
            {
                selectedCard = cardGradeA;
            }
            else if (RatedB())
            {
                selectedCard = cardGradeB;
            }
            else if (RatedC())
            {
                selectedCard = cardGradeC;
            }
            else
            {
                selectedCard = cardGradeF;
            }

            cardPoint.SetCard(selectedCard);
        }

        public bool RatedA()
        {
            return tipjar.GetAmountInJar() >= tipsForA;
        }

        public bool RatedB()
        {
            return tipjar.GetAmountInJar() >= tipsForB;
        }

        public bool RatedC()
        {
            return tipjar.GetAmountInJar() >= tipsForC;
        }

        public bool RatedF()
        {
            return tipjar.GetAmountInJar() < tipsForC;
        }

        public Rating GetRating()
        {
            if (RatedA())
                return Rating.A;
            else if (RatedB())
                return Rating.B;
            else if (RatedC())
                return Rating.C;
            else
                return Rating.F;
        }
    }
}