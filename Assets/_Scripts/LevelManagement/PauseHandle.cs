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
                //LevelManager.Instance.LoadScene(0);
            }
        }

        private bool DoorHandleTurned()
        {
            return gameObject.transform.localEulerAngles.x < 315 && gameObject.transform.localEulerAngles.x > 0;
        }
    }
}