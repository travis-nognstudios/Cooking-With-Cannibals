using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sequence;

namespace Serving
{
    public class SceneTimerClock : MonoBehaviour
    {
        public OrderSpawnerv5 orderSpawner;
        public GameObject clockHand;
        public Image clockRed;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            //UpdateHand();
            UpdateRed();
        }

        void UpdateHand()
        {
            float totalTime = 15 * 60; // Clock is 15 minutes
            float timeLeft = LevelTimeLeft();

            float angle = (timeLeft / totalTime) * 360;
            clockHand.transform.rotation = new Quaternion(angle, 0, 0, 0);
        }

        void UpdateRed()
        {
            float totalTime = 15 * 60; // Clock is 15 minutes
            float timeLeft = LevelTimeLeft();

            float angle = timeLeft / totalTime;
            clockRed.fillAmount = angle;
        }

        private float LevelTimeLeft()
        {
            return orderSpawner.totalServiceTime - orderSpawner.timeSinceServiceStarted;
        }
    }
}