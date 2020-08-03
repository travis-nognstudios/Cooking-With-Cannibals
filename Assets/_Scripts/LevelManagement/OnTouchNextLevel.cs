using UnityEngine;
using System.Collections;
using VRTK;

namespace LevelManagement
{
    public class OnTouchNextLevel : MonoBehaviour
    {
        public PauseManagerv2 pauseManager;
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
                pauseManager.SetNextScene();
            }
        }
    }
}