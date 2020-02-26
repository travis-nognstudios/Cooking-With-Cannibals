using UnityEngine;
using System.Collections;
using VRTK;

namespace LevelManagement
{
    public class GoToEasterEggLocation : MonoBehaviour
    {
        [Header("Pause Management")]
        public PauseManagerv2 pauseManager;

        [Header("Me")]
        public VRTK_InteractableObject interactable;

        void Start()
        {

        }

        void Update()
        {
            if (interactable.IsTouched())
            {
                pauseManager.SetLocationToEasterEgg();
                pauseManager.SetPause();
            }
        }

    }
}