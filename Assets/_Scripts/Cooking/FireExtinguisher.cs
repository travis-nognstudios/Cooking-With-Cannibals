using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneObjects;

namespace Cooking
{
    public class FireExtinguisher : MonoBehaviour
    {
        public ParticleSystem foam;

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Cooktop"))
            {
                if (!foam.isPlaying)
                {
                    foam.Play();
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Cooktop"))
            {
                foam.Stop();
            }
        }
        
    }
}