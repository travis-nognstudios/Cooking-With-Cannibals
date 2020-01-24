using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Serving
{
    public class TipJar : MonoBehaviour
    {
        public int capacity;
        private int amountInJar = 0;

        // Start is called before the first frame update
        void Start()
        {
        
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
        }

        public int GetAmountInJar()
        {
            return amountInJar;
        }
    }
}