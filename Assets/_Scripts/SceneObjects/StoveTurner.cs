using UnityEngine;
using System.Collections;
using VRTK;

namespace SceneObjects
{
    public class StoveTurner : VRTK_InteractableObject
    {
        public override void Ungrabbed(VRTK_InteractGrab previousGrabbingObject = null)
        {
            Debug.Log("Stopped grabbing stove turner");
            Debug.Log($"Current Rotation: {transform.localRotation}");

            
            if (transform.rotation.z > 0.3)
            {
                SnapToOn();
            }
            else
            {
                SnapToOff();
            }
            

            base.Ungrabbed(previousGrabbingObject);
        }

        public void SnapToOff()
        {
            transform.rotation = new Quaternion(0, 0, 0, 1.0f);
        }

        public void SnapToOn()
        {
            transform.rotation = new Quaternion(0, 0, 0.7f, 0.7f);
        }
    }
}