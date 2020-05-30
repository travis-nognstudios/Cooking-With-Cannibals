using System;
using UnityEngine;
using BehaviorTree;
using Serving;

namespace AI
{
    class ReactToKnifeThrow : Node
    {
        private bool alreadyReacted;

        private State myState;

        public override State GetState()
        {
            return myState;
        }

        public override void Run()
        {
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!alreadyReacted)
            {
                if (other.CompareTag("Knife"))
                {
                    Debug.Log("You threw a knife at me jafeel");
                    alreadyReacted = true;
                    myState = State.Success;
                }
            }
            else
            {
                myState = State.Failure;
            }
        }
    }
}
