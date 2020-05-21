using System;
using Serving;
using UnityEngine;

namespace Sequence
{
    public class PlayMinigame : MonoBehaviour, SequenceNode
    {
        public MinigameTeleport minigameTeleport;
        public MinigameManager minigameManager;
        
        public bool IsComplete()
        {
            return minigameManager.Over();
        }

        public void Play()
        {
            minigameTeleport.Teleport();
        }


    }
}