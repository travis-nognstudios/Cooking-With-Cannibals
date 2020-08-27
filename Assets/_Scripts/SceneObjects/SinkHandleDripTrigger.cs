using UnityEngine;
using System.Collections;

namespace SceneObjects
{
    public class SinkHandleDripTrigger : MonoBehaviour
    {
        public float DripTriggerAngle = 30f;
        public bool isOn = false;


        void Update()
        {
            ManageDripTrigger();
        }

        void ManageDripTrigger()
        {
            float yRotation = transform.rotation.eulerAngles.y;

            // If counter-clockwise, normalize the angle
            if (yRotation > 180f && yRotation < 360f)
            {
                float normalized = Mathf.Abs(yRotation - 360f);
                yRotation = normalized;
            }

            if (!isOn && yRotation > DripTriggerAngle)
            {
                isOn = true;
            }
            else if (isOn && yRotation < DripTriggerAngle)
            {
                isOn = false;
            }
        }
    }   
}