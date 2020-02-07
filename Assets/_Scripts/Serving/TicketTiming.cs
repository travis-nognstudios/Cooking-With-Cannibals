using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Serving
{
    public class TicketTiming : MonoBehaviour
    {
        public Image timerImage;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void StartTimer()
        {
            timerImage.fillAmount = 1;
        }

        public void EndTimer()
        {
            timerImage.fillAmount = 0;
        }

        public void UpdateTimer(float totalTime, float timeLeft)
        {
            float angle = timeLeft / totalTime;
            timerImage.fillAmount = angle;
        }
    }
}