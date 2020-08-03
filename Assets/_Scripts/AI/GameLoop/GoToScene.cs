using System;
using UnityEngine;
using BehaviorTree;
using LevelManagement;

namespace AI
{
    class GoToScene : Node
    {
        public PauseManagerv2 pauseManager;
        public int sceneIndex;
        private bool done;

        private State myState;
        
        public override State GetState()
        {
            return myState;
        }

        public override void Run()
        {
            if (!done)
            {
                pauseManager.JumpToScene(sceneIndex);
                done = true;

                myState = State.Success;
            }
        }
    }
}
