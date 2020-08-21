using UnityEngine;
using System.Collections;

namespace SceneObjects
{
    public class SinkDrip : MonoBehaviour
    {
        [Header("Drip")]
        public ParticleSystem dripEffect;
        public float slowDripSpeed = 0.1f;
        public float fastDripSpeed = 0.5f;

        [Header("Sink Handles")]
        public SinkHandleDripTrigger leftHandle;
        public SinkHandleDripTrigger rightHandle;

        //These control the "spread" of the flow near the start
        private float slowStartSpeed = 5f;
        private float fastStartSpeed = 2.5f;


        void Update()
        {
            ManageDrip();
        }

        public void StopDrip()
        {
            //Debug.Log("Stop Drip");

            if (dripEffect.isPlaying)
            {
                dripEffect.Stop();
            }
        }

        public void MakeSlowDrip()
        {
            //Debug.Log("Slow Drip");
            if (dripEffect.isStopped)
            {
                dripEffect.Play();
            }

            ParticleSystem.MainModule effect = dripEffect.main;
            effect.simulationSpeed = slowDripSpeed;
            effect.startSpeed = slowStartSpeed;

        }

        public void MakeFastDrip()
        {
            //Debug.Log("Fast Drip");
            if (dripEffect.isStopped)
            {
                dripEffect.Play();
            }

            ParticleSystem.MainModule effect = dripEffect.main;
            effect.simulationSpeed = fastDripSpeed;
            effect.startSpeed = fastStartSpeed;

        }

        public void ManageDrip()
        {
            // Both on
            if (leftHandle.isOn && rightHandle.isOn)
            {
                MakeFastDrip();
            }
            // One on
            else if (leftHandle.isOn || rightHandle.isOn)
            {
                MakeSlowDrip();
            }
            // Both off
            else
            {
                StopDrip();
            }
        }
    }
}