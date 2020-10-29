using System;
using Serving;
using UnityEngine;

namespace Sequence
{
    public class PlayGameLoop : MonoBehaviour, SequenceNode
    {
        public OrderSpawnerv5 orderSpawner;
        private bool serviceOver;
     
        void Start()
        {
 
        }

        void Update()
        {
            if (!serviceOver)
            {
                serviceOver = orderSpawner.IsServiceOver();
            }
        }

        public bool IsComplete()
        {
            return serviceOver;
        }

        public void Play()
        {
            orderSpawner.StartSpawning();

        }
    }
}