using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Serving
{
    public class TicketClock : MonoBehaviour
    {
        public Image timerImage;
        public Text timerText;
        public Color lowTimeColor;
        public Color highTimeColor;

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
            timerText.text = "";
        }

        public void UpdateTimer(float totalTime, float timeLeft)
        {
            float fillAmount = timeLeft / totalTime;
            timerImage.fillAmount = fillAmount;

            // Time formatted
            string formattedTime;
            string separator = ":";

            int minutes = (int) timeLeft / 60;
            int seconds = (int) timeLeft % 60;

            // Flashing separator
            if (seconds % 2 == 0)
            {
                separator = " ";
            }
            else
            {
                separator = ":";
            }

            // Update time text
            if (minutes > 0)
            {
                formattedTime = $"{minutes}{separator}{seconds.ToString().PadLeft(2, '0')}";
            }
            else
            {
                formattedTime = $"{separator}{seconds.ToString().PadLeft(2, '0')}";
            }

            timerText.text = formattedTime;
            
            // Low time color
            if (timeLeft <= 30)
            {
                timerText.color = lowTimeColor;
            }
            else
            {
                timerText.color = highTimeColor;
            }
        }
        
    }
}