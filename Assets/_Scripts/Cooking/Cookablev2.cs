using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using SceneObjects;

namespace Cooking
{
    public class Cookablev2 : MonoBehaviour
    {

        #region Variables

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
        Renderer rend;

        [Header("UI")]
        public GameObject canvas;
        public CookUI cookUI;
        public Camera cam;

        private CookTop last_touched_cookTop = null;

        #endregion Variables

        void Start()
        {
            // Initialize with uncooked material
            rend = GetComponent<Renderer>();
            rend.enabled = true;
            rend.sharedMaterial = uncookedMat;

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

            // Find camera for cook UI
            if (cam == null)
            {
                GameObject centerEyeAnchor = GameObject.FindWithTag("MainCamera");
                if (centerEyeAnchor != null)
                {
                    cam = centerEyeAnchor.GetComponent<Camera>();
                }
            }
        }

        private void Update()
        {
            if (cam != null)
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

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Cooktop"))
            { 
                canvas.SetActive(true);
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


                if (cookTop.IsHot())
                {
                    cookTimes[typeIndex] += Time.deltaTime;
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

                    // Smoke effect
                    last_touched_cookTop = cookTop;

                    if (isOvercooked)
                    {
                        //cookTop.smoke.burnSmoke();
                        cookTop.GetComponent<Smoke>().BurnSmoke();
                    }
                    else
                    {
                        //cookTop.smoke.cookSmoke();
                        cookTop.GetComponent<Smoke>().CookSmoke();
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

                // Stop smoke
                if (last_touched_cookTop != null)
                {
                    last_touched_cookTop.GetComponent<Smoke>().ClearSmoke();
                    last_touched_cookTop = null;
                }
            }
        }

        private void MakeCooked(CookType cookType)
        {
            // Get current state in allMechanics
            int typeIndex = GetCookTypeIndex(cookType);
            CookMechanic currentState = allMechanics[typeIndex];

            // Change it to Cooked and update allMechanics
            currentState.cookState = CookState.Cooked;
            allMechanics[typeIndex] = currentState;

            // Update look and UI
            rend.sharedMaterial = cookedMat;
            cookUI.SetCookedIcon();
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
            rend.sharedMaterial = burntMat;
            cookUI.SetOvercookedIcon();
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

    }
}