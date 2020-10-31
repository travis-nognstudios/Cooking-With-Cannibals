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
        public float currentOilLevel;
        public float depletionPerSecond;

        [Header("UI")]
        public Image oilLevel1;
        public Image oilLevel2;
        public Image oilLevel3;
        public Image oilLevel4;

        [Header("Oil")]
        public DeepFryer fryerOil;

        public PauseManagerv2 pauseManager;
        public AudioSource deepFryerAudio;

        

        void Start()
        {
            currentOilLevel = maxOilLevel;
        }

        public void UseOil()
        {
            float timeElapsed = pauseManager.DeltaTime();
            float depletion = depletionPerSecond * timeElapsed;

            currentOilLevel -= depletion;
            if (currentOilLevel <= 0)
            {
                currentOilLevel = 0;
            }

            UpdateUI();
            UpdateOilArea();
        }

        void UpdateUI()
        {
            if (currentOilLevel <= 0)
            {
                deepFryerAudio.mute = true;
                oilLevel1.enabled = false;
                oilLevel2.enabled = false;
                oilLevel3.enabled = false;
                oilLevel4.enabled = false;
            }
            else if (currentOilLevel < 1)
            {
                deepFryerAudio.mute = false;
                oilLevel1.enabled = false;
                oilLevel2.enabled = false;
                oilLevel3.enabled = false;
                oilLevel4.enabled = true;
            }
            else if (currentOilLevel < 2)
            {
                deepFryerAudio.mute = false;
                oilLevel1.enabled = false;
                oilLevel2.enabled = false;
                oilLevel3.enabled = true;
                oilLevel4.enabled = true;
            }
            else if (currentOilLevel < 3)
            {
                deepFryerAudio.mute = false;
                oilLevel1.enabled = false;
                oilLevel2.enabled = true;
                oilLevel3.enabled = true;
                oilLevel4.enabled = true;
            }
            else
            {
                deepFryerAudio.mute = false;
                oilLevel1.enabled = true;
                oilLevel2.enabled = true;
                oilLevel3.enabled = true;
                oilLevel4.enabled = true;
            }
        }

        void UpdateOilArea()
        {
            if (currentOilLevel <= 0)
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

                currentOilLevel += amount;

                if (currentOilLevel > maxOilLevel)
                {
                    currentOilLevel = maxOilLevel;
                }
            }
        }
    }
}