using System;
using UnityEngine;
using BehaviorTree;
using Serving;

namespace AI
{
    class Service : Node
    {
        public OrderSpawnerv5 orderSpawner;
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
                orderSpawner.StartSpawning();
                isStarted = true;
            }
            else
            {
                if (orderSpawner.IsServiceOver())
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
