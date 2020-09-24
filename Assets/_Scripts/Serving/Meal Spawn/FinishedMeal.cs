using UnityEngine;
using System.Collections;

namespace Serving
{ 
    public class FinishedMeal : MonoBehaviour
    {
        [Header("Particles")]
        public ParticleSystem finishFX;

        public void PlayFinishFX()
        {
            finishFX.Play();
        }
    }
}