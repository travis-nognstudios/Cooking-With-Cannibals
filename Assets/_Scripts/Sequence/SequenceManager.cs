using UnityEngine;
using System.Collections.Generic;

namespace Sequence
{
    public class SequenceManager : MonoBehaviour
    {
        public MonoBehaviour[] sequenceSteps;
        private List<SequenceNode> nodes = new List<SequenceNode>();

        private int currentNodeIndex;
        private int numNodes;

        void Start()
        {

            foreach(MonoBehaviour step in sequenceSteps)
            {
                nodes.Add(step as SequenceNode);
            }

            numNodes = nodes.Count;
            nodes[0].Play();
        }

        void Update()
        {
            if (nodes[currentNodeIndex].IsComplete() && currentNodeIndex < numNodes-1)
            {
                currentNodeIndex++;
                nodes[currentNodeIndex].Play();
            }
        }
    }
}