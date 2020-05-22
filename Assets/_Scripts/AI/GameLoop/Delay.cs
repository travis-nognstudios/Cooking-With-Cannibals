using System;
using UnityEngine;
using BehaviorTree;

namespace AI
{
    class Delay : Node
    {
        public float seconds;
        private State myState;

        private float timer;
        
        public override State GetState()
        {
            return myState;
        }

        public override void Run()
        {
            timer += Time.deltaTime;

            if (timer >= seconds)
            { 
                myState = State.Success;
                timer = 0;
            }
            else
            {
                myState = State.Failure;
            }
        }
    }
}
