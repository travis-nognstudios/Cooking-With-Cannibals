using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

namespace LevelManagement
{
    public class LevelPickerBar : MonoBehaviour
    {
        public int level;
        public PauseManagerv2 pauseManager;
        public VRTK_InteractableObject interactable;

        private bool alreadyTouched;

        private void Update()
        {
            if (!alreadyTouched && interactable.IsGrabbed())
            {
                alreadyTouched = true;
                GoToScene();
            }
        }

        private void GoToScene()
        {
            pauseManager.JumpToScene(level);
        }

    }
}