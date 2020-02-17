using UnityEngine;
using System.Collections;

namespace Serving
{ 
    public class FinishedMeal : MonoBehaviour
    {
        [Header("Particles")]
        public ParticleSystem finishFX;

        [Header("Tip")]
        public ParticleSystem tipFX_3;
        public ParticleSystem tipFX_2;
        public ParticleSystem tipFX_1;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void PlayFinishFX()
        {
            finishFX.Play();
        }


        public void ShowTipFX(int tipAmount)
        {
            switch (tipAmount)
            {
                case 3:
                    tipFX_3.Play();
                    break;
                case 2:
                    tipFX_2.Play();
                    break;
                case 1:
                    tipFX_1.Play();
                    break;
                default:
                    tipFX_3.Play();
                    break;
            }
        }
    }
}