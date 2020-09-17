using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneObjects;

namespace Cooking
{
    public class CookTop : MonoBehaviour
    {
        private bool hot;
        protected Smoke smoke;
        
        [Header("Cooktop Settings")]
        public CookType cookType;

        public bool hasFood;
        protected bool hasUncookedFood;
        protected bool hasCookingFood;
        protected bool hasBurningFood;

        void Start()
        {
            MakeCold();
            smoke = GetComponent<Smoke>();
        }

        void Update()
        {
            hasFood = hasUncookedFood || hasCookingFood || hasBurningFood;
            ManageSmoke();
        }

        void OnTriggerEnter(Collider other)
        {
            // Hand interactions
            if (other.CompareTag("Hand"))
            {
                HandAnimations anim = other.gameObject.GetComponent<HandAnimations>();
                if (anim != null)
                {
                    anim.PlaySOS();
                }
            }
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

        virtual protected void SyncHeat(HeatSource heatSource)
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

        public void MakeHot()
        {
            hot = true;
        }

        public void MakeCold()
        {
            hot = false;
        }
        
        public bool IsHot()
        {
            return hot;
        }

        public void FoodIsUncooked()
        {
            hasUncookedFood = true;
        }

        public void FoodIsCooking()
        {
            hasCookingFood = true;
        }

        public void FoodIsBurning()
        {
            hasBurningFood = true;
        }

        public void FoodIsLeaving()
        {
            ResetFoodMemory();
        }

        private void ResetFoodMemory()
        {
            hasUncookedFood = false;
            hasCookingFood = false;
            hasBurningFood = false;
        }

        virtual protected void ManageSmoke()
        {
            if (smoke != null)
            {
                if (hasBurningFood)
                {
                    smoke.BurnSmoke();
                }
                else if (hasCookingFood)
                {
                    smoke.CookSmoke();
                }
                else
                {
                    smoke.ClearSmoke();
                }
            }
        }
    }
}