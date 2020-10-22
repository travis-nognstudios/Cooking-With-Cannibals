using System;
using UnityEngine;
using BehaviorTree;
using Serving;

namespace AI
{
    class ServiceBar : Node
    {
        public OrderManagerBar orderManager;
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
                orderManager.isServiceActive = true;
                isStarted = true;
            }
            else
            {
                if (orderManager.isServiceOver)
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
