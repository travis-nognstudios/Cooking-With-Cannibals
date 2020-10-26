using UnityEngine;
using System.Collections;

namespace LevelManagement
{
    public class PauseTimer : MonoBehaviour
    {
        public static bool isPaused;
        public static bool isOutOfFocus;
        public PauseManagerv2 pauseManager;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            isPaused = pauseManager.isPaused;
            isOutOfFocus = pauseManager.isOutOfFocus;
        }

        public static float DeltaTime()
        {
            if (isPaused || isOutOfFocus)
            {
                return 0f;
            }
            else
            {
                return Time.deltaTime;
            }
        }
    }
}