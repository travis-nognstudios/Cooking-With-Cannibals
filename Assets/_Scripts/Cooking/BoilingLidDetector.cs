using System;
using UnityEngine;

namespace Cooking
{
    public class BoilingLidDetector : MonoBehaviour
    {
        public bool hasLid = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("BoilingLid"))
            {
                hasLid = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("BoilingLid"))
            {
                hasLid = false;
            }
        }
    }
}