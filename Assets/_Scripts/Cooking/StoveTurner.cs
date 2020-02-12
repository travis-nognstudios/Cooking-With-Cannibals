using UnityEngine;
using System.Collections;
using VRTK;

namespace Cooking
{
    public class StoveTurner : VRTK_InteractableObject
    {
        #region Variables

        [Header("Turner Settings")]
        public StoveBurner connectedBurner;
        public bool isOn;
        [Range(0.05f, 0.3f)]
        public float snapThreshold = 0.1f;

        private float rotationOnGrab;
        private float rotationOnUngrab;

        #endregion Variables

        public override void Grabbed(VRTK_InteractGrab currentGrabbingObject = null)
        {
            rotationOnGrab = transform.rotation.z;

            base.Grabbed(currentGrabbingObject);
        }
        public override void Ungrabbed(VRTK_InteractGrab previousGrabbingObject = null)
        {
            rotationOnUngrab = transform.rotation.z;
            TriggerSnap();

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
        }

        private void SnapToOn()
        {
            transform.rotation = new Quaternion(0, 0, 0.7f, 0.7f);
            isOn = true;

            connectedBurner.UpdateStove(isOn);
        }
    }
}