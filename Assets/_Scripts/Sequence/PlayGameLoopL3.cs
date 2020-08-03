using System;
using Serving;
using UnityEngine;

namespace Sequence
{
    public class PlayGameLoopL3 : MonoBehaviour, SequenceNode
    {
        public OrderSpawnerGroup orderSpawner;
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
            Debug.Log("Start of Game Loop");
        }
    }
}