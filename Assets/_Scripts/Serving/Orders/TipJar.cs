using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Serving
{
    public class TipJar : MonoBehaviour
    {
        [Header("Tip System")]
        public int capacity;

        [Header("UI")]
        public Text ui;

        [Header("FX")]
        public ParticleSystem tip1;
        public ParticleSystem tip2;
        public ParticleSystem tip3;

        private int amountInJar = 0;

        // Start is called before the first frame update
        void Start()
        {
            UpdateUI();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void AddTip(int amount)
        {
            amountInJar += amount;
            UpdateUI();
            PlayFx(amount);
        }

        public int GetAmountInJar()
        {
            return amountInJar;
        }

        private void UpdateUI()
        {
            ui.text = $"Tip: {amountInJar}/{capacity}";
        }

        private void PlayFx(int amount)
        {
            if (amount == 3)
            {
                tip3.Play();
            }
            else if (amount == 2)
            {
                tip2.Play();
            }
            else if (amount == 1)
            {
                tip1.Play();
            }
        }
    }
}