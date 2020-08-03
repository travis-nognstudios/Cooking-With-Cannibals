using System;
using UnityEngine;
using BehaviorTree;

namespace AI
{
    class HeroIntro : Node
    {
        public HeroIntroOutro hero;
        private bool isStarted;
        private State myState;
        
        public override State GetState()
        {
            return myState;
        }

        public override void Run()
        {
            if (!isStarted)
            {
                hero.PlayIntro();
                isStarted = true;
            }
            else
            {
                if (hero.introDone)
                {
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
