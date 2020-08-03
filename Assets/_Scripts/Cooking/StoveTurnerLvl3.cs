using UnityEngine;
using System.Collections;
using VRTK;

namespace Cooking
{
    public class StoveTurnerLvl3 : VRTK_InteractableObject
    {

        [Header("Turner Settings")]
        public StoveBurner connectedBurner;
        public bool isOn;
        [Range(0.05f, 0.3f)]
        public float snapThreshold = 0.1f;

        private float rotationOnGrab;
        private float rotationOnUngrab;
        public AudioSource audioSource;

        public void Start()
        {
            audioSource = this.GetComponent<AudioSource>();
        }
        
        public override void Grabbed(VRTK_InteractGrab currentGrabbingObject = null)
        {
            rotationOnGrab = transform.rotation.x;
            audioSource.Play();
            base.Grabbed(currentGrabbingObject);
        }
        public override void Ungrabbed(VRTK_InteractGrab previousGrabbingObject = null)
        {
            Debug.Log(transform.rotation);
            rotationOnUngrab = transform.rotation.x;
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
            transform.rotation = new Quaternion(0, 1.0f, 0, 0);
            isOn = false;

            connectedBurner.UpdateStove(isOn);
        }

        private void SnapToOn()
        {
            transform.rotation = new Quaternion(0.7f, 0.7f, 0, 0);
            isOn = true;

            connectedBurner.UpdateStove(isOn);
        }
    }
}