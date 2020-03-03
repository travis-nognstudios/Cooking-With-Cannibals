using UnityEngine;
using System.Collections;
using VRTK;

namespace SceneObjects
{
    public class VibrateOnTouch : MonoBehaviour
    {
        [Header("Settings")]
        [Range(0,1)]
        public float strength = 1;
        [Range(0, 1)]
        public float frequency = 1;

        public VRTK_InteractableObject interactable;
        private bool triggered;

        void Start()
        {
            if (interactable == null)
            {
                interactable = GetComponent<VRTK_InteractableObject>();
            }
        }

        void Update()
        {
            if (interactable.IsTouched() && !triggered)
            {
                PlayVibration();
            }

            if (triggered && !interactable.IsTouched())
            {
                StopVibration();
            }
        }

        void PlayVibration()
        {
            triggered = true;
            OVRInput.SetControllerVibration(frequency, strength, OVRInput.Controller.All);
            StartCoroutine(ResetVibrateTimer());
        }

        void StopVibration()
        {
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.All);
            triggered = false;
        }

        IEnumerator ResetVibrateTimer()
        {
            yield return new WaitForSeconds(2f);
            triggered = false;
        }
    }
}