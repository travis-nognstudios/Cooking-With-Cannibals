using System;
using UnityEngine;

namespace Liquor
{
    class BeerTap : MonoBehaviour
    {
        public GameObject beerArea;
        public ParticleSystem beerFX;

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("BeerTapHandle"))
            {
                TurnOnBeer();
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("BeerTapHandle"))
            {
                TurnOffBeer();
            }
        }

        void TurnOnBeer()
        {
            beerArea.SetActive(true);
            beerFX.Play();
        }

        void TurnOffBeer()
        {
            beerArea.SetActive(false);
            beerFX.Stop();
        }
    }
}
