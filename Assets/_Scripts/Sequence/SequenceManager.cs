using UnityEngine;
using System.Collections.Generic;

namespace Sequence
{
    public abstract class SequenceManager : MonoBehaviour
    {
        protected List<SequenceNode> nodes = new List<SequenceNode>();
        protected int currentNodeIndex;
        protected int numNodes;

        void Start()
        {
            Debug.Log("Starting sequence");

            CreateNodes();
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

        protected abstract void CreateNodes();
    }
}