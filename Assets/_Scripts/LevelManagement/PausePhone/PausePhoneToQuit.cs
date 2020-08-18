using UnityEngine;
using System.Collections;
using VRTK;

namespace LevelManagement
{
    public class PausePhoneToQuit : PausePhoneButtonAction
    {
        override public void DoButtonAction()
        {
            Debug.Log("Quitting Game");

            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
}