using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using VRTK;

namespace LevelManagement
{
    public class PausePhoneButtonOnTouch : VRTK_InteractableObject
    {
        [Header("Pause Button Actions")]
        public float secondsTouchedToActivate = 2.0f;
        public PausePhoneButtonAction buttonAction;

        [Header("UI")]
        public Image pressUI;

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
            touching = true;
            ShowUI();

            base.StartTouching(currentTouchingObject);
        }

        public override void StopTouching(VRTK_InteractTouch previousTouchingObject = null)
        {
            touching = false;
            activationTimer = 0f;
            HideUI();

            base.StopTouching(previousTouchingObject);
        }

        private void DoButtonAction()
        {
            buttonAction.DoButtonAction();
        }

        private void ShowUI()
        {
            pressUI.enabled = true;
        }

        private void HideUI()
        {
            pressUI.enabled = false;
        }
    }
}