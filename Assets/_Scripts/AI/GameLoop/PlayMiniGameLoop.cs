using System;
using UnityEngine;
using BehaviorTree;
using Serving;

namespace AI
{
    class PlayMiniGameLoop : Node
    {
        [Header("Mini Game")]
        public MinigameTeleport minigameTeleport;
        public MinigameManager minigameManager;

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
                minigameTeleport.Teleport();
                isStarted = true;
            }
            else
            {
                if (minigameManager.Over())
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
