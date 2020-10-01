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


        private Rigidbody rb;
        private Vector3 position;
        private Vector3 positionLastFrame;
        private Vector3 velocity;
        private float speed;

        private VRTK_InteractableObject interactable;
        private AudioSource audioSource;

        private bool playedSoundThisSwoosh; // Only play sound once per swoosh

        void Start()
        {
            interactable = GetComponent<VRTK_InteractableObject>();
            rb = GetComponent<Rigidbody>();
            audioSource = GetComponent<AudioSource>();
            positionLastFrame = rb.position;
        }

        void Update()
        {
            PlayAudioBasedOnVelocity();
        }

        void FixedUpdate()
        {
            CalculateCustomVelocityBecauseVRTKisWeirdAboutIt();
        }

        void PlayAudioBasedOnVelocity()
        {
            bool isGrabbed = interactable.IsGrabbed();

            if (isGrabbed)
            {
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

        void CalculateCustomVelocityBecauseVRTKisWeirdAboutIt()
        {
            position = rb.position;
            velocity = position - positionLastFrame;
            speed = velocity.magnitude;
            positionLastFrame = position;
        }
    }
}