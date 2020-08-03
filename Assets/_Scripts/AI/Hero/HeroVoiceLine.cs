using System;
using UnityEngine;
using BehaviorTree;

namespace AI
{
    class HeroVoiceLine : Node
    {
        [Header("Reaction")]
        public AudioSource audioSource;
        public AudioClip voiceClip;

        private State myState;

        [Header("Timing")]
        private float voiceLineTime;
        private float timer;
        private bool started;
        private bool finished;
       
        void Start()
        {
            voiceLineTime = voiceClip.length;
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
                audioSource.clip = voiceClip;
                audioSource.Play();
            }

            if (!finished)
            {
                timer += Time.deltaTime;

                if (timer >= voiceLineTime)
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
