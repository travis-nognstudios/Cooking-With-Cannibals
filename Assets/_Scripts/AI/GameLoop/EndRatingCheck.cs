using System;
using UnityEngine;
using BehaviorTree;
using Serving;

namespace AI
{
    class EndRatingCheck : Node
    {
        [Header("Rating")]
        public RatingCardSpawner rating;
        public String checkForGrade;

        private State myState;
        
        public override State GetState()
        {
            return myState;
        }

        public override void Run()
        {
            if (ReachedGrade())
            {
                myState = State.Success;
            }
            else
            {
                myState = State.Failure;
            }
        }

        private bool ReachedGrade()
        {
            if (checkForGrade.ToUpper().Equals("A"))
            {
                return rating.RatedA();
            }
            else if (checkForGrade.ToUpper().Equals("B"))
            {
                return rating.RatedB();
            }
            else if (checkForGrade.ToUpper().Equals("C"))
            {
                return rating.RatedC();
            }
            else
            {
                return false;
            }
        }
    }
}
