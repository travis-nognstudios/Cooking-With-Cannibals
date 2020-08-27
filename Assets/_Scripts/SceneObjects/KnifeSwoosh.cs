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
        private Rigidbody rb;
        private AudioSource audioSource;

        private bool playedSoundThisSwoosh; // Only play sound once per swoosh

        void Start()
        {
            interactable = GetComponent<VRTK_InteractableObject>();
            rb = GetComponent<Rigidbody>();
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