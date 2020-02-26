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
            //Debug.Log($"Handle rotation: {gameObject.transform.localEulerAngles.x}");

            if (DoorHandleTurned())
            {
                pauseManager.SetPause();
            }
        }

        private bool DoorHandleTurned()
        {
            return gameObject.transform.localEulerAngles.x < 315 && gameObject.transform.localEulerAngles.x > 270;
        }
    }
}