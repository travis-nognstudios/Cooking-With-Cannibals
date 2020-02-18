using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cooking
{

    public abstract class HeatSource : MonoBehaviour
    {

        protected bool isOn;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        public abstract void TurnOn();

        public abstract void TurnOff();

        public bool IsOn()
        {
            return isOn;
        }
    }
}