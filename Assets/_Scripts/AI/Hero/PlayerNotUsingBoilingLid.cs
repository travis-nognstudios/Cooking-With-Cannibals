using System;
using UnityEngine;
using BehaviorTree;
using Cooking;

namespace AI
{
    class PlayerNotUsingBoilingLid : Node
    {
        [Header("TipJar")]
        public CookTopBoil boilingPot;

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
                if (boilingPot.firstTimeAddingFood)
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
