using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SceneObjects
{
    public class ArmDodge : MonoBehaviour
    {
        public Rigidbody arm;

        [Range(0f, 5f)]
        public float forceMultiplier = 0.75f;

        private readonly float baseForce = -500; // Opposite direction to knife

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Knife"))
            {
                Vector3 myPosition = transform.position;
                Vector3 knifePosition = other.transform.position;
                Vector3 knifeDirection = knifePosition - myPosition;

                Vector3 dodgeForce = knifeDirection * baseForce * forceMultiplier;

                //Debug.Log($"Knife coming from: {knifeDirection}");
                //arm.AddForce(dodgeForce);
                arm.AddForceAtPosition(dodgeForce, myPosition);
            }
        }
    }
}
