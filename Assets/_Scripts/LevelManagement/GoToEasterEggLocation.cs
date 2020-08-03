using UnityEngine;
using System.Collections;
using VRTK;
using LemonAidRush;

namespace LevelManagement
{
    public class GoToEasterEggLocation : MonoBehaviour
    {
        [Header("Managers")]
        public PauseManagerv2 pauseManager;
        public RushOrderSpawner rushSpawner;

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

                rushSpawner.StartRushMode();
            }
        }

    }
}