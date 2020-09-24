using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneObjects;

namespace Cooking
{
    public class SmokeScreen : MonoBehaviour
    {
        public ParticleSystem kitchenSmoke;
        private bool isSmoking;

        public int numSmokeScreensStarted;
        public int numSmokeScreensStopped;

        public void StartSmokeScreen()
        {
            if (!isSmoking)
            {
                isSmoking = true;
                kitchenSmoke.Play();

                numSmokeScreensStarted++;
                Debug.Log($"Smokescreens: {numSmokeScreensStarted}");
            }
        }

        public void StopSmokeScreen()
        {
            if (isSmoking)
            {
                isSmoking = false;
                kitchenSmoke.Stop();

                numSmokeScreensStopped++;
            }
        }
    }
}