using System;
using UnityEngine;
using BehaviorTree;
using Serving;

namespace AI
{
    class ServiceOverBar : Node
    {
        public OrderManagerBar orderManager;
        private State myState;
        
        public override State GetState()
        {
            return myState;
        }

        public override void Run()
        {
            if (orderManager.isServiceOver)
            {
                myState = State.Success;
            }
            else
            {
                myState = State.Failure;
            }
        }
    }
}
