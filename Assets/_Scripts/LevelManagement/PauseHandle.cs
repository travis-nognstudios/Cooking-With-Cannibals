using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement
{
    public class PauseHandle : MonoBehaviour
    {
        public PauseManagerv2 pauseManager;

        private float minTurnAngle = 45f;
        private float maxTurnAngle = 90f;

        void Update()
        {
            if (DoorHandleTurned())
            {
                Debug.Log("Door Handle Turned");
                pauseManager.SetPause();
            }
        }

        private float NormalizedRotation(float angle)
        {
            if (angle > 180f && angle < 360f)
            {
                angle = Mathf.Abs(angle - 360f);
            }

            return angle;
        }

        private bool AngleInTurningRange(float angle)
        {
            return angle > minTurnAngle && angle < maxTurnAngle;
        }

        private bool DoorHandleTurned()
        {
            float x = NormalizedRotation(transform.localEulerAngles.x);
            float z = NormalizedRotation(transform.localEulerAngles.z);

            bool x_turned = AngleInTurningRange(x);
            bool z_turned = AngleInTurningRange(z);

            return x_turned || z_turned;

            //bool x_turned = gameObject.transform.localEulerAngles.x < 315 && gameObject.transform.localEulerAngles.x > 270;
            //bool y_turned = gameObject.transform.localEulerAngles.y < 315 && gameObject.transform.localEulerAngles.y > 270;
            //bool z_turned = gameObject.transform.localEulerAngles.z < 315 && gameObject.transform.localEulerAngles.z > 270;
            //return x_turned || y_turned || z_turned;
        }
    }
}