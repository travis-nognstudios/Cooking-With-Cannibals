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

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void StartSmokeScreen()
        {
            if (!isSmoking)
            {
                isSmoking = true;
                kitchenSmoke.Play();
            }
        }

        public void StopSmokeScreen()
        {
            if (isSmoking)
            {
                isSmoking = false;
                kitchenSmoke.Stop();
            }
        }
    }
}