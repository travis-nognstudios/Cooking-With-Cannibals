using UnityEngine;
using System.Collections;
using VRTK;

namespace LevelManagement
{
    public class PausePhoneToMainMenu : PausePhoneButtonAction
    {
        public PauseManagerv2 pauseManager;

        override public void DoButtonAction()
        {
            Debug.Log("Going to Main Menu");
            pauseManager.JumpToScene(0);
        }
    }
}