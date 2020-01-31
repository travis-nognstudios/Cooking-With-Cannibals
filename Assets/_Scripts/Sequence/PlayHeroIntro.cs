using System;
using AI;
using UnityEngine;

namespace Sequence
{
    public class PlayHeroIntro : MonoBehaviour, SequenceNode
    {
        public HeroIntroOutro hero;
        private bool introCompleted;

        void Start()
        {

        }

        void Update()
        {
            if (!introCompleted)
            {
                introCompleted = hero.introDone;
            }
        }

        public bool IsComplete()
        {
            return introCompleted;
        }

        public void Play()
        {
            hero.PlayIntro();
        }


    }
}