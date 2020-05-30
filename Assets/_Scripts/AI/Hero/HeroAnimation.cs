using System;
using UnityEngine;
using BehaviorTree;

namespace AI
{
    class HeroAnimation : Node
    {
        [Header("Reaction")]
        public Animator animator;
        public string triggerName;

        private State myState;
       
        void Start()
        {

        }

        public override State GetState()
        {
            return myState;
        }

        public override void Run()
        {
            animator.SetTrigger(triggerName);
            myState = State.Success;
        }

        
    }
}
