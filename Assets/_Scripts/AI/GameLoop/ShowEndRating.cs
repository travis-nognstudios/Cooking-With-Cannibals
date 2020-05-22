using System;
using UnityEngine;
using BehaviorTree;
using Serving;

namespace AI
{
    class ShowEndRating : Node
    {
        public RatingCardSpawner rating;
        private State myState;
        
        public override State GetState()
        {
            return myState;
        }

        public override void Run()
        {
            rating.SpawnCard();
            myState = State.Success;
        }
    }
}
