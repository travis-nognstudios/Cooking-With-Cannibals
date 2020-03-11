using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement
{
    public class PauseHandle : MonoBehaviour
    {
        public PauseManagerv2 pauseManager;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (DoorHandleTurned())
            {
                pauseManager.SetPause();
            }
        }

        private bool DoorHandleTurned()
        {
            bool x_turned = gameObject.transform.localEulerAngles.x < 315 && gameObject.transform.localEulerAngles.x > 270;
            bool y_turned = gameObject.transform.localEulerAngles.y < 315 && gameObject.transform.localEulerAngles.y > 270;
            bool z_turned = gameObject.transform.localEulerAngles.z < 315 && gameObject.transform.localEulerAngles.z > 270;
            return x_turned || y_turned || z_turned;
        }
    }
}