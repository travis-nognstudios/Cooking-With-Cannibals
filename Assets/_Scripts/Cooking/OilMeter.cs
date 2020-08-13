using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelManagement;
using UnityEngine.UI;

namespace Cooking
{
    public class OilMeter : MonoBehaviour
    {
        [Header("Rates")]
        public float maxOilLevel = 4;
        public float depletionPerSecond;

        [Header("UI")]
        public Image oilLevel1;
        public Image oilLevel2;
        public Image oilLevel3;
        public Image oilLevel4;

        [Header("Oil")]
        public DeepFryer fryerOil;

        public PauseManagerv2 pauseManager;

        private float oilLevel;

        void Start()
        {
            oilLevel = maxOilLevel;
        }

        public void UseOil()
        {
            float timeElapsed = pauseManager.DeltaTime();
            float depletion = depletionPerSecond * timeElapsed;

            oilLevel -= depletion;
            if (oilLevel <= 0)
            {
                oilLevel = 0;
            }

            UpdateUI();
            UpdateOilArea();
        }

        void UpdateUI()
        {
            if (oilLevel <= 0)
            {
                oilLevel1.enabled = false;
                oilLevel2.enabled = false;
                oilLevel3.enabled = false;
                oilLevel4.enabled = false;
            }
            else if (oilLevel < 1)
            {
                oilLevel1.enabled = false;
                oilLevel2.enabled = false;
                oilLevel3.enabled = false;
                oilLevel4.enabled = true;
            }
            else if (oilLevel < 2)
            {
                oilLevel1.enabled = false;
                oilLevel2.enabled = false;
                oilLevel3.enabled = true;
                oilLevel4.enabled = true;
            }
            else if (oilLevel < 3)
            {
                oilLevel1.enabled = false;
                oilLevel2.enabled = true;
                oilLevel3.enabled = true;
                oilLevel4.enabled = true;
            }
            else
            {
                oilLevel1.enabled = true;
                oilLevel2.enabled = true;
                oilLevel3.enabled = true;
                oilLevel4.enabled = true;
            }
        }

        void UpdateOilArea()
        {
            if (oilLevel <= 0)
            {
                fryerOil.TurnOff();
            }
            else
            {
                fryerOil.TurnOn();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("OilRefill"))
            {
                int amount = other.GetComponent<OilRefill>().refillAmount;
                Destroy(other.gameObject);

                oilLevel += amount;

                if (oilLevel > maxOilLevel)
                {
                    oilLevel = maxOilLevel;
                }
            }
        }
    }
}