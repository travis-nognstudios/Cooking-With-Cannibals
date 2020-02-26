using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cooking
{

    public class DeepFryer : HeatSource
    {
        [Header("Refill Mechanic")]
        public float maxFillAmount;
        public float currentFillAmount;
        public GameObject refillItem;
        public int refillItemFillValue;

        // Start is called before the first frame update
        void Start()
        {
            TurnOn();
        }

        // Update is called once per frame
        void Update()
        {
            if (!isOn)
            {
                TurnOn();
            }

            Debug.Log($"Deepfryer is On: {isOn}");
        }

        public override void TurnOn()
        {
            isOn = true;
        }

        public override void TurnOff()
        {
            isOn = false;
        }
        
    }
}