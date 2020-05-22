using System;
using UnityEngine;
using BehaviorTree;
using SceneObjects;

namespace AI
{
    class OpenAwning : Node
    {
        public Awning awning;
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
                awning.OpenAwning();
                isStarted = true;
            }
            else
            {
                if (awning.isOpen)
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
