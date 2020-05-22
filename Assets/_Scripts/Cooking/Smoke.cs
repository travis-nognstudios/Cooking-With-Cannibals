using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelManagement;

namespace Cooking
{
    public class Smoke : MonoBehaviour
    {
        [Header("Cooktop Smoke")]
        public ParticleSystem cookingSmoke;
        public ParticleSystem burningSmoke;
        public ParticleSystem burningFire;

        [Header("Smokescreen")]
        public SmokeScreen smokescreen;
        public float timeToPutOutSmokescreen;
        private float extinguisherTouchingTime;

        void Start()
        {
            cookingSmoke.Stop();
            burningSmoke.Stop();
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("FireExtinguisher"))
            {
                extinguisherTouchingTime += PauseTimer.DeltaTime();
                if (extinguisherTouchingTime >= timeToPutOutSmokescreen)
                {
                    smokescreen.StopSmokeScreen();
                    extinguisherTouchingTime = 0;
                }
            }
        }

        public void ClearSmoke()
        {
            cookingSmoke.Stop();
            burningSmoke.Stop();
            burningFire.Stop();
        }

        public void CookSmoke()
        {
            // Burn smoke takes precedence
            if (!cookingSmoke.isPlaying && !burningSmoke.isPlaying)
            {
                cookingSmoke.Play();
            }
        }

        public void BurnSmoke()
        {
            if (cookingSmoke.isPlaying)
            {
                cookingSmoke.Stop();
            }

            if (!burningSmoke.isPlaying)
            {
                burningSmoke.Play();
                burningFire.Play();
                smokescreen.StartSmokeScreen();
            }
        }

    }
}