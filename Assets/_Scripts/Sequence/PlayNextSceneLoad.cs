using System;
using Serving;
using UnityEngine;
using LevelManagement;

namespace Sequence
{
    
    public class PlayNextSceneLoad : MonoBehaviour, SequenceNode
    {

        public PauseManagerv2 pauseManager;

        void Start()
        {

        }

        void Update()
        {
            
        }

        public bool IsComplete()
        {
            return false;
        }

        public void Play()
        {
            pauseManager.SetNextScene();
        }
    }
}