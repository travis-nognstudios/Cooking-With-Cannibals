using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
public class QuitButton : MonoBehaviour
{
    public VRTK_InteractableObject interactableObject;
    private void Update()
    {
        if (interactableObject.IsTouched())
        {

#if UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
        }
    }
}
