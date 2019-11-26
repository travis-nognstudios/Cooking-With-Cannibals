using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Cooking
{
    public class Cookablev2 : MonoBehaviour
    {

        #region Variables

        
        private List<CookMechanic> steps;
        private List<CookMechanic> allMechanics;
        private List<float> cookTimes;

        [Header("Cook Times")]
        public CookTime[] stateChangeTimes;

        [Header("Textures")]
        public Material uncookedMat;
        public Material cookedMat;
        public Material burntMat;
        Renderer rend;



        #endregion Variables


        // Use this for initialization
        void Start()
        {
            // ==============================
            // ==============================
            // Testing Only
            // Remove later
            rend = GetComponent<Renderer>();
            rend.enabled = true;
            rend.sharedMaterial = uncookedMat;
            // ==============================
            // ==============================


            // Instantiate objects
            steps = new List<CookMechanic>();
            allMechanics = new List<CookMechanic>();
            cookTimes = new List<float>();

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

        // Update is called once per frame
        void Update()
        {
           
        }

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

        // Cook
        void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Cooktop"))
            {

                // Cooktop properties
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
  

                    if (!isCooked && timeCooked >= timeToCook && timeCooked < timeToOvercook)
                    {
                        MakeCooked(cookTopType);
                        steps.Add(allMechanics[typeIndex]);
                    }
                    else if (!isOvercooked && timeCooked >= timeToOvercook)
                    {
                        MakeOvercooked(cookTopType);
                        //ToDo Update steps for this type
                    }
                }
            }
        }

        private void MakeCooked(CookType cookType)
        {
            Debug.Log("cooked" + cookType);
            int typeIndex = GetCookTypeIndex(cookType);
            CookMechanic currentState = allMechanics[typeIndex];
            currentState.cookState = CookState.Cooked;

            allMechanics[typeIndex] = currentState;

            //ToDo Change later
            rend.sharedMaterial = cookedMat;
        }

        private void MakeOvercooked(CookType cookType)
        {

            int typeIndex = GetStepIndex(cookType);
            CookMechanic currentState = steps[typeIndex];
            currentState.cookState = CookState.Burnt;

            steps[typeIndex] = currentState;

            //ToDo Change later
            rend.sharedMaterial = burntMat;
        }
    }
}