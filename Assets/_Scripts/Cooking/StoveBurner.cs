using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneObjects;

namespace Cooking
{

    public class StoveBurner : HeatSource
    {
        [Header("FX")]
        public ParticleSystem flame;
        public AudioSource audioSource;


        // Start is called before the first frame update
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
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
        public override void TurnOn()
        {
            flame.Play();
            audioSource.Play();
            isOn = true;
        }

        public override void TurnOff()
        {
            flame.Stop();
            audioSource.Stop();
            isOn = false;
        }
        
    }
}