using System;
using UnityEngine;
using BehaviorTree;
using Cooking;

namespace AI
{
    class GotSmokeScreen : Node
    {
        [Header("TipJar")]
        public SmokeScreen smokeScreen;
        public int nthTime;

        private bool alreadyReacted;

        private State myState;

        public override State GetState()
        {
            return myState;
        }

        public override void Run()
        {
            if (!alreadyReacted)
            {
                int nthSmokeScreen = smokeScreen.numSmokeScreensStarted;
                if (nthSmokeScreen == nthTime)
                {
                    myState = State.Success;
                    alreadyReacted = true;
                }
                else
                {
                    myState = State.Failure;
                }
            }
            else
            {
                myState = State.Failure;
            }
        }
    }
}
