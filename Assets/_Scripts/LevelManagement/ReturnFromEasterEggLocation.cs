using UnityEngine;
using System.Collections;
using VRTK;

namespace LevelManagement
{
    public class ReturnFromEasterEggLocation : MonoBehaviour
    {
        [Header("Pause Management")]
        public PauseManagerv2 pauseManager;
        public Transform lemonadeStand;

        private Transform originalPausePosition;

        [Header("Me")]
        public VRTK_InteractableObject interactable;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (interactable.IsTouched())
            {
                Teleport();
            }
        }

        void Teleport()
        {
            originalPausePosition = pauseManager.pausePoint;
            pauseManager.pausePoint = lemonadeStand;
            pauseManager.SetPause();
        }
    }
}