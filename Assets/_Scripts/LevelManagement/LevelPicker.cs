using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneObjects;
using Cooking;

namespace LevelManagement
{
    public class LevelPicker : MonoBehaviour
    {
        public int level;
        public PauseManagerv2 pauseManager;
        public PosterDissolve posterDissolve;

        private bool hot;
        private bool triggered;
        
        void Start()
        {
            MakeCold();
        }

        private void Update()
        {

        }
     
        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Heatsource"))
            {
                MakeCold();
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Heatsource"))
            {
                HeatSource heatSource = other.gameObject.GetComponent<HeatSource>();
                SyncHeat(heatSource);
            }
        }

        private void SyncHeat(HeatSource heatSource)
        {
            if (heatSource.IsOn() && !IsHot())
            {
                MakeHot();
            }
            else if (!heatSource.IsOn() && IsHot())
            {
                MakeCold();
            }
        }

        private void MakeHot()
        {
            if (!triggered)
            {
                hot = true;
                triggered = true;

                PlayDissolveEffect();
                GoToScene();
            }
        }

        private void MakeCold()
        {
            hot = false;
        }
        
        public bool IsHot()
        {
            return hot;
        }

        private void GoToScene()
        {
            pauseManager.JumpToScene(level);
        }

        private void PlayDissolveEffect()
        {
            posterDissolve.isDissolving = true;
        }
    }
}