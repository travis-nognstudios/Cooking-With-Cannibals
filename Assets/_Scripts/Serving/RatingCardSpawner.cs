using UnityEngine;
using System.Collections;

namespace Serving
{
    public class RatingCardSpawner : MonoBehaviour
    {
        [Header("Rating Cards")]
        public GameObject bestRatingCard;
        public GameObject goodRatingCard;
        public GameObject failRatingCard;

        [Header("Ratings By Tip")]
        public int perfectIfAboveTip;
        public int failIfBelowTip;

        [Header("Scene Objects")]
        public TipJar tipjar;
        public RatingCardPoint cardPoint;
        public ParticleSystem particals;
        public int num;

        // Use this for initialization
        void Start()
        {
            GameObject selectedCard;
            int tip = tipjar.GetAmountInJar();

            if (tip >= perfectIfAboveTip)
            {
                selectedCard = bestRatingCard;
            }
            else if (tip <= failIfBelowTip)
            {
                selectedCard = failRatingCard;
            }
            else
            {
                selectedCard = goodRatingCard;
            }

            particals.Emit(num);
            cardPoint.SetCard(selectedCard);
        }

        // Update is called once per frame
        void Update()
        {

        }
        /*
        public void SpawnCard()
        {
            GameObject selectedCard;
            int tip = tipjar.GetAmountInJar();

            if (tip >= perfectIfAboveTip)
            {
                selectedCard = bestRatingCard;
            }
            else if (tip <= failIfBelowTip)
            {
                selectedCard = failRatingCard;
            }
            else
            {
                selectedCard = goodRatingCard;
            }

            particals.Emit(num);
            cardPoint.SetCard(selectedCard);
        }
        */
    }
}