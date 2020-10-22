using System;
using UnityEngine;
using BehaviorTree;
using Serving;

namespace AI
{
    class ShowEndRatingBar : Node
    {
        public RatingManagerBar ratingManager;
        private State myState;
        
        public override State GetState()
        {
            return myState;
        }

        public override void Run()
        {
            ratingManager.ShowRatingCard();
            myState = State.Success;
        }
    }
}
