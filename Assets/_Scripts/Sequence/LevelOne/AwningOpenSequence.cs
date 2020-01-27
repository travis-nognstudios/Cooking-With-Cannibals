using UnityEngine;
using SceneObjects;

namespace Sequence
{
    public class AwningOpenSequence : MonoBehaviour, SequenceNode
    {
        public Awning awning;

        void Start()
        {

        }

        void Update()
        {

        }

        public bool IsComplete()
        {
            return awning.isOpen;
        }

        public void Play()
        {
            awning.OpenAwning();
        }
    }
}