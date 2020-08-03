using System;
using Serving;
using UnityEngine;

namespace Sequence
{
    public class PlayRatingSpawn : MonoBehaviour, SequenceNode
    {
        public RatingCardSpawner spawner;

        void Start()
        {

        }

        void Update()
        {
            
        }

        public bool IsComplete()
        {
            return true;
        }

        public void Play()
        {
            spawner.SpawnCard();
        }


    }
}