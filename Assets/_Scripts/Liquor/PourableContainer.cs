using System;
using UnityEngine;

namespace Liquor
{
    class PourableContainer : MonoBehaviour
    {
        public GameObject liquidFill;
        public string filledBy;
        public Glass glass;

        void OnTriggerEnter(Collider other)
        {
            if (!glass.hasLiquid && other.CompareTag("Liquor"))
            {
                string liquorName = other.gameObject.name;
                bool isCorrectLiquor = liquorName.Equals(filledBy);

                if (isCorrectLiquor)
                {
                    FillCup();
                }
            }
        }

        private void FillCup()
        {
            liquidFill.SetActive(true);
            glass.hasLiquid = true;
            glass.AddToGlass(filledBy);
        }
    }
}
