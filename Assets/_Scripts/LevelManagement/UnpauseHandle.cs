using UnityEngine;
using System.Collections;

namespace LevelManagement
{
    public class UnpauseHandle : MonoBehaviour
    {
        public PauseManagerv2 pauseManager;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (DoorHandleTurned())
            {
                pauseManager.SetUnpause();
            }
        }

        private bool DoorHandleTurned()
        {
            return gameObject.transform.localEulerAngles.x < 315 && gameObject.transform.localEulerAngles.x > 0;
        }
    }
}