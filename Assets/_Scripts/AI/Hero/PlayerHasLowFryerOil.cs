using System;
using UnityEngine;
using BehaviorTree;
using Cooking;

namespace AI
{
    class PlayerHasLowFryerOil : Node
    {
        [Header("Oil")]
        public OilMeter oilMeter;
        public float oilThreshold = 1f;

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
                if (oilMeter.currentOilLevel < oilThreshold)
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
