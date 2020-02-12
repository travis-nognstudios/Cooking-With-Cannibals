using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneObjects;

namespace Cooking
{

    public class StoveBurner : MonoBehaviour
    {
        #region Variables

        [Header("FX")]
        public ParticleSystem flame;

        private bool isOn;

        #endregion Variables

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void UpdateStove(bool on)
        {
            if (on)
            {
                TurnOn();
            }
            else
            {
                TurnOff();
            }
        }

        private void TurnOn()
        {
            flame.Play();
            isOn = true;
        }

        private void TurnOff()
        {
            flame.Stop();
            isOn = false;
        }

        public bool IsOn()
        {
            return isOn;
        }
    }
}