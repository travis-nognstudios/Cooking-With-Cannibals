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

        void Start()
        {
            MakeCold();
        }

        private void Update()
        {
            Debug.Log($"{gameObject.name} is hot:{hot}");
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
            hot = true;
        }

        private void MakeCold()
        {
            hot = false;
        }


        public bool IsHot()
        {
            return hot;
        }
    }
}