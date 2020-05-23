using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cooking
{

    public class DeepFryer : HeatSource
    {
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