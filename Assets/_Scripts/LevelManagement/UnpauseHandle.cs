using UnityEngine;
using System.Collections;

namespace LevelManagement
{
    public class UnpauseHandle : MonoBehaviour
    {
        public PauseManagerv2 pauseManager;

        private float minTurnAngle = 45f;
        private float maxTurnAngle = 90f;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (DoorHandleTurned())
            {
                Debug.Log("Door Handle Turned");
                pauseManager.SetUnpause();
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
        }
    }
}