using System;
using UnityEngine;

namespace Liquor
{
    class PourableContainer : MonoBehaviour
    {
        public GameObject liquidFill;
        public string filledBy;
        public bool singleUse;

        void Start()
        {
            
        }

        void Update()
        {
            
        }

        void OnTriggerEnter(Collider other)
        {
            if (IsEmpty() && other.CompareTag("Liquor"))
            {
                string name = other.gameObject.name;

                if (name.Equals(filledBy))
                {
                    FillCup();
                }

                /*
                if (singleUse)
                {
                    Destroy(other);
                }
                */
            }
        }

        private void FillCup()
        {
            liquidFill.SetActive(true);
        }

        private bool IsEmpty()
        {
            return liquidFill.activeSelf == false;
        }
    }
}
