using UnityEngine;
using System.Collections;
using VRTK;
using SceneObjects;

namespace Cooking
{
    public class StoveTurnerLvl3 : VRTK_InteractableObject
    {

        [Header("Turner Settings")]
        public StoveBurner connectedBurner;
        public bool isOn;
        [Range(5f, 45f)]
        public float snapThreshold = 10f;
        public StoveIndicatorLight indicatorLight;

        private float rotationOnGrab;
        private float rotationOnUngrab;
        //public AudioSource audioSource;

        private float offRotation = 0;
        private float onRotation = 270;

        public void Start()
        {
            //audioSource = this.GetComponent<AudioSource>();
        }
        
        public override void Grabbed(VRTK_InteractGrab currentGrabbingObject = null)
        {
            rotationOnGrab = transform.localEulerAngles.z;

            //audioSource.Play();
            base.Grabbed(currentGrabbingObject);
        }

        public override void Ungrabbed(VRTK_InteractGrab previousGrabbingObject = null)
        {
            rotationOnUngrab = transform.localEulerAngles.z;

            TriggerSnap();
            //audioSource.Stop();
            base.Ungrabbed(previousGrabbingObject);
        }

        private void TriggerSnap()
        {
            float rotationDifference = normalizedRotationAngle(rotationOnUngrab) - normalizedRotationAngle(rotationOnGrab);
            bool rotationTowardsOn = rotationDifference < 0;

            // Snap to new position
            if (Mathf.Abs(rotationDifference) > snapThreshold)
            {
                if (rotationTowardsOn)
                {
                    SnapToOn();
                }
                else
                {
                    SnapToOff();
                }
            }
            else // Snap back to current position
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
            float x = transform.localEulerAngles.x;
            float y = transform.localEulerAngles.y;
            float z = offRotation;

            transform.localEulerAngles = new Vector3(x,y,z);
            isOn = false;

            connectedBurner.UpdateStove(isOn);
            indicatorLight.TurnOff();
        }

        private void SnapToOn()
        {
            float x = transform.localEulerAngles.x;
            float y = transform.localEulerAngles.y;
            float z = onRotation;

            transform.localEulerAngles = new Vector3(x, y, z);
            isOn = true;

            connectedBurner.UpdateStove(isOn);
            indicatorLight.TurnOn();
        }

        // Normalize 0 degrees to 360 degrees so angle differences are consistent
        private float normalizedRotationAngle(float angle)
        {
            if (angle == 0)
            {
                return 360;
            }

            return angle;
        }
    }
}