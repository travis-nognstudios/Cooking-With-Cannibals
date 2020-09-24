using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cooking
{

    public abstract class HeatSource : MonoBehaviour
    {

        protected bool isOn;

        public abstract void TurnOn();

        public abstract void TurnOff();

        public bool IsOn()
        {
            return isOn;
        }
    }
}