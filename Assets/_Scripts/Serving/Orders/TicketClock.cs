using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Serving
{
    public class TicketClock : MonoBehaviour
    {
        [Header("Elements")]
        public Image timerImage;
        public Text timerText;

        [Header("Colors")]
        public Color lowTimeColor;
        public Color highTimeColor;

        [Header("VIP")]
        public bool isVIP;
        public Color vipColor;

        public void StartTimer(bool isVIP)
        {
            timerImage.fillAmount = 1;
            this.isVIP = isVIP;
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
            string separator;

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

            // Low time / high time colors
            if (!isVIP)
            {
                if (timeLeft <= 30)
                {
                    timerText.color = lowTimeColor;
                }
                else
                {
                    timerText.color = highTimeColor;
                }
            }
            // VIP has one color -> High Priority
            else
            {
                timerText.color = vipColor;
            }
        }
        
    }
}