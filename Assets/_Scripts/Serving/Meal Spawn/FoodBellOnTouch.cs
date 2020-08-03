using UnityEngine;
using VRTK;

namespace Serving
{
    public class FoodBellOnTouch : VRTK_InteractableObject
    {
        private FoodBellRing bell;

        public override void StartTouching(VRTK_InteractTouch currentTouchingObject)
        {
            // On first touch, remember bell
            if (bell == null)
            {
                bell = GetComponent<FoodBellRing>();
            }

            Debug.Log("Hit bell");
            bell.MakeReady();

            base.StartTouching(currentTouchingObject);
        }

        public override void StopTouching(VRTK_InteractTouch previousTouchingObject = null)
        {
            bell.MakeNotReady();

            base.StopTouching(previousTouchingObject);
        }

    }
}