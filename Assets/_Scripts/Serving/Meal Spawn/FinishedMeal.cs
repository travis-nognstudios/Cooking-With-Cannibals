using UnityEngine;
using System.Collections;

namespace Serving
{ 
    public class FinishedMeal : MonoBehaviour
    {
        [Header("Particles")]
        public ParticleSystem finishFX;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void PlayFinishFX()
        {
            finishFX.Play();
        }
    }
}