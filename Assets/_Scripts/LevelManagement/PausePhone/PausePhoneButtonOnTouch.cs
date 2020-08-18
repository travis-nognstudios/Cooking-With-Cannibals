using UnityEngine;
using System.Collections;
using VRTK;

namespace LevelManagement
{
    public class PausePhoneButtonOnTouch : VRTK_InteractableObject
    {
        [Header("Pause Button Actions")]
        public float secondsTouchedToActivate = 2.0f;
        public PausePhoneButtonAction buttonAction;

        private float activationTimer = 0f;
        private bool activated = false;

        private bool touching = false;

        private void Update()
        {
            if (touching && !activated)
            {
                activationTimer += Time.deltaTime;

                if (activationTimer > secondsTouchedToActivate)
                {
                    activated = true;
                    DoButtonAction();
                }
            }
        }

        public override void StartTouching(VRTK_InteractTouch currentTouchingObject)
        {
            Debug.Log($"Start touching button: {gameObject.name}");
            touching = true;

            base.StartTouching(currentTouchingObject);
        }

        public override void StopTouching(VRTK_InteractTouch previousTouchingObject = null)
        {
            Debug.Log($"Stop touching button: {gameObject.name}");
            touching = false;
            activationTimer = 0f;

            base.StopTouching(previousTouchingObject);
        }

        private void DoButtonAction()
        {
            buttonAction.DoButtonAction();
        }
    }
}