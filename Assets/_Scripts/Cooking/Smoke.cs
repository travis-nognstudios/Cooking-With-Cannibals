using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cooking
{
    public class Smoke : MonoBehaviour
    {

        public ParticleSystem cookingSmoke;
        public ParticleSystem burningSmoke;

        void Start()
        {
            cookingSmoke.Stop();
            burningSmoke.Stop();
        }


        public void ClearSmoke()
        {
            cookingSmoke.Stop();
            burningSmoke.Stop();
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
            }
        }

    }
}