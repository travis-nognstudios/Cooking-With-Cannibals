using UnityEngine;

namespace Serving
{
    public class FoodBellRing : MealReadyCheck
    {
        public override void MakeNotReady()
        {
            isReady = false;
        }

        public override void MakeReady()
        {
            isReady = true;
        }

    }
}