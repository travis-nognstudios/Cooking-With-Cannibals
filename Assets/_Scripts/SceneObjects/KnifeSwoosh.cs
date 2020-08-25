using UnityEngine;
using System.Collections;
using VRTK;

namespace SceneObjects
{
    public class KnifeSwoosh : MonoBehaviour
    {

        public AudioClip swooshSound;
        public float thresholdSpeed = 0.2f;
        public float resetSpeed = 0.1f;

        private VRTK_InteractableObject interactable;
        private AudioSource audioSource;

        private bool playedSoundThisSwoosh; // Only play sound once per swoosh

        void Start()
        {
            interactable = GetComponent<VRTK_InteractableObject>();
            audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            PlayAudioBasedOnVelocity();
        }

        void PlayAudioBasedOnVelocity()
        {
            bool isGrabbed = interactable.IsGrabbed();

            if (isGrabbed)
            {
                // Child-of-controller grab means my speed is hidden by grabber speed
                // So get the grabbing hand's speed instead
                GameObject grabbingHand = interactable.GetGrabbingObject();
                Rigidbody rb = grabbingHand.GetComponent<Rigidbody>();

                float speed = rb.velocity.magnitude;

                if (speed > thresholdSpeed && !playedSoundThisSwoosh)
                {
                    PlayClip();
                    playedSoundThisSwoosh = true;
                }
                else if (speed < resetSpeed)
                {
                    playedSoundThisSwoosh = false;
                }
            }
        }

        void PlayClip()
        {
            audioSource.clip = swooshSound;
            audioSource.Play();
        }
    }
}