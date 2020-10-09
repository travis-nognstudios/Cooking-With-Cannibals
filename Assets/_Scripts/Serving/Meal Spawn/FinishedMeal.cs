using UnityEngine;
using VRTK;

namespace Serving
{ 
    public class FinishedMeal : MonoBehaviour
    {
        [Header("FX")]
        public ParticleSystem particleFX;
        public Animator floatAnimator;

        private VRTK_InteractableObject interactable;
        private bool hasBeenGrabbed;

        // Use this for initialization
        void Start()
        {
            interactable = GetComponent<VRTK_InteractableObject>();
        }

        // Update is called once per frame
        void Update()
        {
            CheckIfHasBeenGrabbed();
            StopFloatingOnceGrabbed();
        }

        public void PlayFinishFX()
        {
            particleFX.Play();
        }

        private void CheckIfHasBeenGrabbed()
        {
            if (!hasBeenGrabbed && interactable.IsGrabbed())
            {
                hasBeenGrabbed = true;
            }
        }

        private void StopFloatingOnceGrabbed()
        {
            if (hasBeenGrabbed)
                floatAnimator.enabled = false;
        }
    }
}