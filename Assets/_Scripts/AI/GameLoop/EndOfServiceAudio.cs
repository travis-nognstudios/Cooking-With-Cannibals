using System;
using UnityEngine;
using BehaviorTree;
using Serving;

namespace AI
{
    class EndOfServiceAudio : Node
    {
        [Header("Sounds")]
        public AudioSource levelMusic;
        public AudioSource audioSource;
        public AudioClip fanfare;

        private float fanfareTime;

        private bool started;
        private float fanfareTimer;
        private bool finished;

        private State myState;

        void Start()
        {
            fanfareTime = fanfare.length;
        }

        public override State GetState()
        {
            return myState;
        }

        public override void Run()
        {
            if (!started)
            {
                started = true;
                levelMusic.Stop();

                audioSource.clip = fanfare;
                audioSource.Play();
            }
            
            if (!finished)
            {
                fanfareTimer += Time.deltaTime;

                if (fanfareTimer >= fanfareTime)
                {
                    finished = true;
                    myState = State.Success;
                }
                else
                {
                    myState = State.Running;
                }
            }
            
        }
    }
}
