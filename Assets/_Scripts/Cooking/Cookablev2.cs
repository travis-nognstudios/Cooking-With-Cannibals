using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LevelManagement;

namespace Cooking
{
    public class Cookablev2 : MonoBehaviour
    {
        /*
        steps = all the cook steps made on this object
        allMechanics = all possible cooking that can be done

        Usage:
        allMechanics is used to check the progress of the cook states
        when a state change happens, it's updated on steps
        cookTimes tracks the times on each CORRESPONDING allMechanics element
        */

        private List<CookMechanic> steps = new List<CookMechanic>();
        private List<CookMechanic> allMechanics = new List<CookMechanic>();
        private List<float> cookTimes = new List<float>();

        [Header("Cook Times")]
        public CookTime[] stateChangeTimes;

        [Header("Textures")]
        public Material uncookedMat;
        public Material cookedMat;
        public Material burntMat;
        
        private Renderer rend;
        private Material currentStateMat;

        [Header("UI")]
        public GameObject canvas;
        public CookUIv2 cookUI;
        private Camera cam;

        private CookTop last_touched_cookTop = null;


        void Start()
        {
            // Initialize with uncooked material
            rend = GetComponent<Renderer>();
            rend.enabled = true;
            
            rend.material = uncookedMat;
            currentStateMat = uncookedMat;

            // Populate cookmechanics and their respective cooktimes
            foreach(CookType cooktype in Enum.GetValues(typeof(CookType)))
            {
                CookMechanic mechanic = new CookMechanic
                {
                    cookType = cooktype,
                    cookState = CookState.Uncooked
                };

                allMechanics.Add(mechanic);
                cookTimes.Add(0);
            }
        }

        private void Update()
        {
            if (cam == null)
            {
                cam = Camera.main;
            }
            else
            {
                cookUI.transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward);
            }
        }

        // Get the index on allMechanics given the cookType
        private int GetCookTypeIndex(CookType lookingFor)
        {
            for(int i=0; i<allMechanics.Count; ++i)
            {
                CookType cooktype = allMechanics[i].cookType;

                if (cooktype.Equals(lookingFor))
                {
                    return i;
                }
            }

            return -1;
        }

        // Get the index on cookTime (corresponding to allMechanics) given the cookType
        private int GetCookTimeIndex(CookType lookingFor)
        {
            for (int i = 0; i < stateChangeTimes.Length; ++i)
            {
                CookType cooktype = stateChangeTimes[i].cookType;

                if (cooktype.Equals(lookingFor))
                {
                    return i;
                }
            }

            return -1;
        }

        // Get the index on steps given the cookType
        private int GetStepIndex(CookType lookingFor)
        {
            for (int i = 0; i < steps.Count; ++i)
            {
                CookType cooktype = steps[i].cookType;

                if (cooktype.Equals(lookingFor))
                {
                    return i;
                }
            }

            return -1;
        }

        void SetCookUI(CookType cookType)
        {
            int i = GetCookTimeIndex(cookType);
            float cookTime = stateChangeTimes[i].timeToCook;
            float overcookTime = stateChangeTimes[i].timeToOverCook;

            cookUI.SetBackground(cookTime, overcookTime);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Cooktop"))
            {
                CookType cookType = other.gameObject.GetComponent<CookTop>().cookType;
                SetCookUI(cookType);
            }
        }

        // Cook
        void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Cooktop"))
            {
                // Get cooktop's type
                CookTop cookTop = other.gameObject.GetComponent<CookTop>();
                CookType cookTopType = cookTop.cookType;
                int typeIndex = GetCookTypeIndex(cookTopType);


                // State change properties
                int timeIndex = GetCookTimeIndex(cookTopType);
                float timeToCook = stateChangeTimes[timeIndex].timeToCook;
                float timeToOvercook = stateChangeTimes[timeIndex].timeToOverCook;

                // Current state
                CookState currentState = allMechanics[typeIndex].cookState;
                bool isCooked = currentState.Equals(CookState.Cooked);
                bool isOvercooked = currentState.Equals(CookState.Burnt);

                // Keep cooktop updated with cook state
                // so it can do things like setting off the right smoke
                if (isCooked)
                {
                    cookTop.FoodIsCooking();
                }
                else if (isOvercooked)
                {
                    cookTop.FoodIsBurning();
                }
                else
                {
                    cookTop.FoodIsUncooked();
                }

                // UI
                ManageCookUIVisibility();

                // Cook
                if (cookTop.IsHot())
                {
                    cookTimes[typeIndex] += PauseTimer.DeltaTime();
                    float timeCooked = cookTimes[typeIndex];

                    // Side effects
                    cookUI.UpdateFill(timeToOvercook, timeCooked);
                    PlayCookingSound();
        
                    // If cook state reached, update allMechanics and add to steps
                    if (!isCooked && timeCooked >= timeToCook && timeCooked < timeToOvercook)
                    {
                        MakeCooked(cookTopType);
                        steps.Add(allMechanics[typeIndex]);
                    }
                    else if (!isOvercooked && timeCooked >= timeToOvercook)
                    {
                        
                        MakeOvercooked(cookTopType);
                    }
                }
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Cooktop"))
            {
                canvas.SetActive(false);
                StopCookingSound();
                other.GetComponent<CookTop>().FoodIsLeaving();
            }
        }

        public void MakeCooked(CookType cookType)
        {
            // Get current state in allMechanics
            int typeIndex = GetCookTypeIndex(cookType);
            CookMechanic currentState = allMechanics[typeIndex];

            // Change it to Cooked and update allMechanics
            currentState.cookState = CookState.Cooked;
            allMechanics[typeIndex] = currentState;

            // Update look and UI
            rend.material = cookedMat;
            currentStateMat = cookedMat;
        }

        private void MakeOvercooked(CookType cookType)
        {
            // Get current state in allMechanics
            int typeIndexAllMechanics = GetCookTypeIndex(cookType);
            CookMechanic currentState = allMechanics[typeIndexAllMechanics];

            // Change it to Burnt and update allMechanics
            currentState.cookState = CookState.Burnt;
            allMechanics[typeIndexAllMechanics] = currentState;

            // Also update steps
            int typeIndexSteps = GetStepIndex(cookType);
            steps[typeIndexSteps] = currentState;

            // Update look and UI
            rend.material = burntMat;
            currentStateMat = burntMat;
        }

        public void PlayCookingSound()
        {
            AudioSource audioSource = GetComponent<AudioSource>();

            if (audioSource != null)
            {
                GetComponent<GrabBasedAudio>().SetCookSound();
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
        }

        public void StopCookingSound()
        {
            AudioSource audioSource = GetComponent<AudioSource>();

            if (audioSource != null)
            {
                GetComponent<GrabBasedAudio>().SetDropSound();
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }
            }
        }

        public List<CookMechanic> GetSteps()
        {
            return this.steps;
        }

        public Material GetCurrentStateMat()
        {
            return currentStateMat;
        }

        private void ManageCookUIVisibility()
        {
            // On by default, only turns off when exiting
            if (!canvas.activeSelf)
            {
                canvas.SetActive(true);
            }
        }
    }
}