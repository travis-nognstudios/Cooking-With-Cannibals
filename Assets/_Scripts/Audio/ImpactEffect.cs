using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

namespace Audio
{
    public class ImpactEffect : VRTK.VRTK_InteractableObject
    {
        public float dropSpeed = 1f;
        public bool isPlayingSomething;

        

        public override void OnInteractableObjectGrabbed(InteractableObjectEventArgs e)
        {
            base.OnInteractableObjectGrabbed(e);
            GrabBasedAudio audio = GetComponent<GrabBasedAudio>();

            if (audio != null)
            {
                audio.SetInteractSound();
            }
        }


        public override void OnInteractableObjectUngrabbed(InteractableObjectEventArgs e)
        {
            base.OnInteractableObjectUngrabbed(e);
            GrabBasedAudio audio = GetComponent<GrabBasedAudio>();
            if (audio != null)
            {
                GetComponent<GrabBasedAudio>().SetDropSound();
            }
        }

        public override void Grabbed(VRTK_InteractGrab other)
        {
            GrabBasedAudio audio = GetComponent<GrabBasedAudio>();
            if (audio != null)
            {
                audio.SetInteractSound();
            }
        }


        public override void Ungrabbed(VRTK_InteractGrab other)
        {
            GrabBasedAudio audio = GetComponent<GrabBasedAudio>();
            if (audio != null)
            {
                audio.SetDropSound();
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.relativeVelocity.magnitude > dropSpeed)
            {
                GrabBasedAudio audio = GetComponent<GrabBasedAudio>();

                if (audio != null)
                {
                    StartCoroutine("PlayCollisionSFX");
                }
            }
        }

        IEnumerator PlayCollisionSFX()
        {
            GrabBasedAudio audio = GetComponent<GrabBasedAudio>();
            isPlayingSomething = true;
            audio.soundSource.Play();
            yield return new WaitForSeconds(audio.dropSound.length);
            isPlayingSomething = false;
        }
    } 
}
