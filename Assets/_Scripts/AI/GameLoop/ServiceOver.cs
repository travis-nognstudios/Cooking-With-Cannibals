using System;
using UnityEngine;
using BehaviorTree;
using Serving;

namespace AI
{
    class ServiceOver : Node
    {
        public OrderSpawnerv5 orderSpawner;
        private State myState;
        
        public override State GetState()
        {
            return myState;
        }

        public override void Run()
        {
            if (orderSpawner.IsServiceOver())
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
