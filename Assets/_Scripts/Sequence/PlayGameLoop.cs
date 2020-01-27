using System;
using Serving;
using UnityEngine;

namespace Sequence
{
    public class PlayGameLoop : MonoBehaviour, SequenceNode
    {
        public OrderSpawnerv5 orderSpawner;
        private bool allOrdersCompleted;

        void Start()
        {

        }

        void Update()
        {
            if (!allOrdersCompleted)
            {
                allOrdersCompleted = CheckAllComplete();
            }
        }

        public bool IsComplete()
        {
            if (allOrdersCompleted)
            {
                Debug.Log("End of Game loop");
            }

            return allOrdersCompleted;
        }

        public void Play()
        {
            orderSpawner.StartSpawning();
            Debug.Log("Start of Game Loop");
        }

        private bool CheckAllComplete()
        {
            return orderSpawner.numberOfTickets == orderSpawner.GetNumTicketsCompleted();
        }
    }
}