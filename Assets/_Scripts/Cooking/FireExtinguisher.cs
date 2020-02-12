using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneObjects;

namespace Cooking
{
    public class FireExtinguisher : MonoBehaviour
    {
        #region Variables

        public ParticleSystem foam;

        #endregion Variables

        void Start()
        {
            
        }

        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Cooktop"))
            {
                foam.Play();
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