using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Serving
{
    public class RatingCardBar : MonoBehaviour
    {
        [Header("Grade")]
        public Text letterA;
        public Text letterB;
        public Text letterC;
        public Text letterF;

        [Header("Timing")]
        public Text minutes;
        public Text seconds;
        public Text milliseconds;

        public void SetGrade(string achievedGrade)
        {
            if (achievedGrade == "A")
                letterA.enabled = true;
            else if (achievedGrade == "B")
                letterB.enabled = true;
            else if (achievedGrade == "C")
                letterC.enabled = true;
            else
                letterF.enabled = true;
        }

        public void SetTime(float totalSeconds)
        {

            int minutes_time = (int) totalSeconds / 60;
            int seconds_time = Mathf.FloorToInt(totalSeconds);
            int milliseconds_time = (int) ((totalSeconds - Mathf.Floor(totalSeconds)) * 100);

            minutes.text = minutes_time.ToString().PadLeft(2, '0');
            seconds.text = seconds_time.ToString().PadLeft(2, '0');
            milliseconds.text = milliseconds_time.ToString().PadLeft(2,'0').Substring(0,2);
        }
    }
}