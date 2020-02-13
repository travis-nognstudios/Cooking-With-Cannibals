using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneObjects;

namespace Cooking
{
    public class CookTop : MonoBehaviour
    {
        #region Variables

        private bool hot;

        [Header("Cooktop Settings")]
        public CookType cookType;
        public Smoke smoke;

        #endregion Variables

        // Start is called before the first frame update
        void Start()
        {
            MakeCold();
        }

        // Update is called once per frame
        void Update()
        {

        }

        
        void OnTriggerEnter(Collider other)
        {
            /*
            // Stove interactions
            if (other.gameObject.CompareTag("Heatsource"))
            {
                StoveBurner burner = other.gameObject.GetComponent<StoveBurner>();

                if (burner.IsOn())
                {
                    MakeHot();
                }
            }
            */

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
                StoveBurner burner = other.gameObject.GetComponent<StoveBurner>();
                SyncHeat(burner);
            }
        }

        private void SyncHeat(StoveBurner burner)
        {
            
            if (burner.IsOn() && !IsHot())
            {
                MakeHot();
            }
            else if (!burner.IsOn() && IsHot())
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