using UnityEngine;
using System.Collections;

namespace Serving
{
    public class RatingCardSpawner : MonoBehaviour
    {
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
        public RatingCardPoint cardPoint;

        [Header("Audio")]
        public AudioSource audioSource;
        public AudioClip tootyHorn;

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }
        
        public void SpawnCard()
        {
            audioSource.clip = tootyHorn;
            audioSource.Play();

            GameObject selectedCard;
            int tip = tipjar.GetAmountInJar();

            if (tip >= tipsForA)
            {
                selectedCard = cardGradeA;
            }
            else if (tip >= tipsForB)
            {
                selectedCard = cardGradeB;
            }
            else if (tip >= tipsForC)
            {
                selectedCard = cardGradeC;
            }
            else
            {
                selectedCard = cardGradeF;
            }

            cardPoint.SetCard(selectedCard);
        }
    }
}