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
            pauseManager.JumpToScene(0);
        }
    }
}