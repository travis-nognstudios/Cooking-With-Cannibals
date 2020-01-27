using UnityEngine;
using System.Collections;

namespace AI
{
    public class HeroIntroOutro : MonoBehaviour
    {

        private enum OutroType { BEST, GOOD, BAD }

        private OutroType outroType;

        public float introTime;
        public float bestOutroTime;
        public float goodOutroTime;
        public float badOutroTime;

        private float timeTracker;

        [HideInInspector]
        public bool introDone;
        [HideInInspector]
        public bool outroDone;

        [HideInInspector]
        public bool isPlayingIntro;
        [HideInInspector]
        public bool isPlayingOutro;

        void Start()
        {

        }

        void Update()
        {
            if (isPlayingIntro)
            {
                timeTracker += Time.deltaTime;
                if (timeTracker >= introTime)
                {
                    isPlayingIntro = false;
                    introDone = true;
                    timeTracker = 0f;

                    Debug.Log("Hero Intro Done");
                }
            }
            else if (isPlayingOutro)
            {
                timeTracker += Time.deltaTime;
                float outroTime = GetOutroTime();

                if (timeTracker >= outroTime)
                {
                    isPlayingOutro = false;
                    outroDone = true;
                    timeTracker = 0f;

                    Debug.Log("Hero Outro Done");
                }
            }
        }

        public void PlayIntro()
        {
            Debug.Log("Playing Hero Intro");

            isPlayingIntro = true;
            timeTracker = 0f;
        }

        public void PlayOutro()
        {
            Debug.Log("Playing Hero Outro");

            // ToDo: choose outro properly
            PlayGoodOutro();

            isPlayingOutro = true;
            timeTracker = 0f;
        }

        public void PlayBestOutro()
        {
            outroType = OutroType.BEST;
        }

        public void PlayGoodOutro()
        {
            outroType = OutroType.GOOD;
        }

        public void PlayBadOutro()
        {
            outroType = OutroType.BAD;
        }

        private float GetOutroTime()
        {
            if (outroType == OutroType.BEST)
            {
                return bestOutroTime;
            }
            else if (outroType == OutroType.GOOD)
            {
                return goodOutroTime;
            }
            else if (outroType == OutroType.BAD)
            {
                return badOutroTime;
            }
            else
            {
                return 0f;
            }
        }
    }
}