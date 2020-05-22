using System;
using UnityEngine;
using BehaviorTree;
using LevelManagement;

namespace AI
{
    class GoToNextLevel : Node
    {
        public PauseManagerv2 pauseManager;
        private State myState;
        
        public override State GetState()
        {
            return myState;
        }

        public override void Run()
        {
            pauseManager.SetNextScene();
            myState = State.Success;
        }
    }
}
