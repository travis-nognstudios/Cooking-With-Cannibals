using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelManagement;
using UnityEngine.UI;

namespace Cooking
{
    public class OilRefill : MonoBehaviour
    {
        public int refillAmount;
        public OilRefillIndicator[] refillIndicators;

        private void Start()
        {
            ShowRefillIndicator(refillAmount);
        }

        private void ShowRefillIndicator(int n)
        {
            foreach (OilRefillIndicator indicator in refillIndicators)
            {
                if (n == 2)
                {
                    indicator.ticks2.SetActive(true);
                }
                else if (n == 3)
                {
                    indicator.ticks3.SetActive(true);
                }
            }
        }
    }
}