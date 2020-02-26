using System;
using Serving;
using UnityEngine;

namespace Sequence
{
    public class PlayEndingSound : MonoBehaviour, SequenceNode
    {
        public AudioSource levelMusic;
        public AudioSource audioSource;
        public AudioClip fanfare;

        private float fanfareTime;

        private float fanfareTimer;
        private bool fanfareStarted;
        private bool fanfareDone;

        void Start()
        {
            fanfareTime = fanfare.length;
        }

        void Update()
        {
            if (fanfareStarted && !fanfareDone)
            {
                fanfareTimer += Time.deltaTime;
                if (fanfareTimer >= fanfareTime)
                {
                    fanfareDone = true;
                }
            }
        }

        public bool IsComplete()
        {
            return fanfareDone;
        }

        public void Play()
        {
            levelMusic.Stop();
            fanfareStarted = true;

            audioSource.clip = fanfare;
            audioSource.Play();
        }


    }
}