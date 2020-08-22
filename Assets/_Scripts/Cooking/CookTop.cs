using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneObjects;

namespace Cooking
{
    public class CookTop : MonoBehaviour
    {
        private bool hot;
        
        [Header("Cooktop Settings")]
        public CookType cookType;

        [Header("Food Interactions")]
        public bool hasFood;

        void Start()
        {
            MakeCold();
        }

        void Update()
        {
            //CheckHasFood();
        }

        void CheckHasFood()
        {
            if (hasFood)
            {
                hasFood = false;
            }
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

            if (other.gameObject.layer == 0)
            {
                hasFood = false;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Heatsource"))
            {
                HeatSource heatSource = other.gameObject.GetComponent<HeatSource>();
                SyncHeat(heatSource);
            }

            if (other.gameObject.layer == 0)
            {
                hasFood = true;
            }
        }

        virtual protected void SyncHeat(HeatSource heatSource)
        {
            if (heatSource.IsOn() && !IsHot())
            {
                MakeHot();
            }
            else
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
    }
}