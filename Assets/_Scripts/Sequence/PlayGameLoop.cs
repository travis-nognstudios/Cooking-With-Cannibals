using System;
using Serving;
using UnityEngine;

namespace Sequence
{
    public class PlayGameLoop : MonoBehaviour, SequenceNode
    {
        public OrderSpawnerv5 orderSpawner;
        private bool allOrdersCompleted;

        [Range(0,10)]
        public float timeLimitInMinutes;
        private float timeLimitInSeconds;
        private bool isTimed;

        private float timer;
        private bool timeLimitReached;


        void Start()
        {
            isTimed = timeLimitInMinutes != 0;
            timeLimitInSeconds = timeLimitInMinutes * 60;
        }

        void Update()
        {
            if (isTimed && !timeLimitReached)
            {
                timer += Time.deltaTime;

                if (timer >= timeLimitInSeconds)
                {
                    timeLimitReached = true;
                }
            }

            if (!allOrdersCompleted)
            {
                allOrdersCompleted = CheckAllComplete();
            }
        }

        public bool IsComplete()
        {
            bool complete;

            if (isTimed)
            {
                complete = allOrdersCompleted || timeLimitReached;
            }
            else
            {
                complete = allOrdersCompleted;
            }

            if (complete)
            {
                Debug.Log("End of Game loop");

                orderSpawner.StopSpawning();
                orderSpawner.RemoveAllTickets();
            }

            return complete;
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