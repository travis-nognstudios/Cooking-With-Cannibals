using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cooking
{

    public class DeepFryer : HeatSource
    {
        [Header("Oil")]
        public GameObject oilArea;
        public OilMeter oilMeter;

        [Header("Food Interactions")]
        public CookTop fryingBasket;

        // Start is called before the first frame update
        void Start()
        {
            TurnOn();
        }

        // Update is called once per frame
        void Update()
        {
            UseOil();
        }
        
        public override void TurnOn()
        {
            if (!isOn)
            {
                isOn = true;
                oilArea.SetActive(true);
            }
        }

        public override void TurnOff()
        {
            if (isOn)
            {
                isOn = false;
                oilArea.SetActive(false);
            }
        }
        
        private void UseOil()
        {
            if (fryingBasket.hasFood)
            {
                oilMeter.UseOil();
            }
        }
    }
}