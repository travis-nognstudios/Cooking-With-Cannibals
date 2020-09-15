using UnityEngine;
using System.Collections;
using VRTK;
using SceneObjects;

namespace Cooking
{
    public class StoveTurnerMainMenu : VRTK_InteractableObject
    {

        [Header("Turner Settings")]
        public StoveBurner connectedBurner;
        public bool isOn;
        [Range(0.05f, 0.3f)]
        public float snapThreshold = 0.1f;
        public StoveIndicatorLight indicatorLight;

        private float rotationOnGrab;
        private float rotationOnUngrab;
        public AudioSource audioSource;

        private Quaternion offPosition = new Quaternion(0, 0, 0, 1.0f);
        private Quaternion onPosition = new Quaternion(0, 0, -0.7f, 0.7f);

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
                // Direction depends on orientation of the turner
                if (rotationDifference < 0)
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
            transform.rotation = offPosition;
            isOn = false;
            connectedBurner.UpdateStove(isOn);
            indicatorLight.TurnOff();
        }

        private void SnapToOn()
        {
            transform.rotation = onPosition;
            isOn = true;
            connectedBurner.UpdateStove(isOn);
            indicatorLight.TurnOn();
        }
    }
}