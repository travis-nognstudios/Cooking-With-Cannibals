using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

namespace Audio
{
    public class ImpactEffect : VRTK.VRTK_InteractableObject
    {
        public float dropSpeed = 1f;

       public override void OnInteractableObjectGrabbed(InteractableObjectEventArgs e)
        {
            base.OnInteractableObjectGrabbed(e);
            Debug.Log("Im Grabbed");
            GetComponent<GrabBasedAudio>().SetInteractSound();
        }


        public override void OnInteractableObjectUngrabbed(InteractableObjectEventArgs e)
        {
            base.OnInteractableObjectUngrabbed(e);
            Debug.Log("Not grabbed");
            GetComponent<GrabBasedAudio>().SetDropSound();
        }

        public virtual void Grabbed(VRTK_InteractGrab other)
        {

            Debug.Log("Im Grabbed method");
            GetComponent<GrabBasedAudio>().SetInteractSound();
        }


        public virtual void Ungrabbed(VRTK_InteractGrab other)
        {
            Debug.Log("Not grabbed method");
            GetComponent<GrabBasedAudio>().SetDropSound();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.relativeVelocity.magnitude > dropSpeed)
            {
                GetComponent<GrabBasedAudio>().soundSource.Play();
                Debug.Log("Something should be playing");
            }
        }
    }
}
