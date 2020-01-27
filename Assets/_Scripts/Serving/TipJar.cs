using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Serving
{
    public class TipJar : MonoBehaviour
    {
        public int capacity;
        private int amountInJar = 0;

        public Text ui;

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

            // Don't overfill
            if (amountInJar > capacity)
            {
                amountInJar = capacity;
            }

            UpdateUI();
        }

        public int GetAmountInJar()
        {
            return amountInJar;
        }

        private void UpdateUI()
        {
            ui.text = $"Tip: {amountInJar}/{capacity}";
        }
    }
}