﻿using System;
using UnityEngine;
using BehaviorTree;
using Serving;

namespace AI
{
    class GotSilverTip : Node
    {
        [Header("TipJar")]
        public TipJar tipJar;
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
                int nthGoldTip = tipJar.numSilverTips;
                if (nthGoldTip == nthTime)
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