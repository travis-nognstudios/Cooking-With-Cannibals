using System;
using UnityEngine;

namespace Serving
{
    public class TicketUILine : MonoBehaviour
    {
        public TicketAmountUI[] AmountUI;
        public int amount;

        private void Start()
        {
            
        }

        private void Update()
        {
            
        }

        public void SetUI()
        {
            for (int i=0; i< AmountUI.Length; ++i)
            {
                if (amount == AmountUI[i].amount)
                {
                    AmountUI[i].UI.SetActive(true);
                }
            }
        }
    }
}