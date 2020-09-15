using UnityEngine;
using System.Collections;
using VRTK;
using SceneObjects;

namespace Cooking
{
    public class StoveTurner : VRTK_InteractableObject
    {

        [Header("Turner Settings")]
        public StoveBurner connectedBurner;
        public bool isOn;
        [Range(0.05f, 0.3f)]
        public float snapThreshold = 0.1f;
        public StoveIndicatorLight indicatorLight;

        private float rotationOnGrab;
        private float rotationOnUngrab;
        private AudioSource audioSource;

        public void Start()
        {
            audioSource = this.GetComponent<AudioSource>();
        }


        public override void Grabbed(VRTK_InteractGrab currentGrabbingObject = null)
        {
            rotationOnGrab = transform.rotation.z;
            audioSource.Play();
            base.Grabbed(currentGrabbingObject);
        }
        public override void Ungrabbed(VRTK_InteractGrab previousGrabbingObject = null)
        {
            rotationOnUngrab = transform.rotation.z;
            TriggerSnap();
            audioSource.Stop();
            base.Ungrabbed(previousGrabbingObject);
        }

        private void TriggerSnap()
        {
            float rotationDifference = rotationOnUngrab - rotationOnGrab;

            if (Mathf.Abs(rotationDifference) > snapThreshold)
            {
                if (rotationDifference > 0)
                {
                    SnapToOn();
                }
                else
                {
                    SnapToOff();
                }
            }
            else
            {
                if (isOn)
                {
                    SnapToOn();
                }
                else
                {
                    SnapToOff();
                }
            }
        }

        private void SnapToOff()
        {
            transform.rotation = new Quaternion(0, 0, 0, 1.0f);
            isOn = false;

            connectedBurner.UpdateStove(isOn);
            indicatorLight.TurnOff();
        }

        private void SnapToOn()
        {
            transform.rotation = new Quaternion(0, 0, 0.7f, 0.7f);
            isOn = true;

            connectedBurner.UpdateStove(isOn);
            indicatorLight.TurnOn();
        }
    }
}