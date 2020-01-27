using System;
using AI;
using UnityEngine;

namespace Sequence
{
    public class PlayHeroOutro : MonoBehaviour, SequenceNode
    {
        public HeroIntroOutro hero;
        private bool outroCompleted;

        void Start()
        {

        }

        void Update()
        {
            if (!outroCompleted)
            {
                outroCompleted = hero.outroDone;
            }
        }

        public bool IsComplete()
        {
            return outroCompleted;
        }

        public void Play()
        {
            hero.PlayOutro();
        }


    }
}