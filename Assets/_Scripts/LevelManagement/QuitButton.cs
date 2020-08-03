using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

namespace LevelManagement
{
    public class QuitButton : MonoBehaviour
    {
        public VRTK_InteractableObject interactableObject;
        private void Update()
        {
            if (interactableObject.IsTouched())
            {

            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
            }
        }
    }
}