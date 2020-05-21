using System;
using UnityEngine;

namespace BehaviorTree
{
    class Selector : Composite
    {
        public Node[] nodes;
        private int nodeIndex;
        private int numNodes;

        private State myState;

        void Start()
        {
            numNodes = nodes.Length;
        }

        public override void Run()
        {
            Node currentNode = nodes[nodeIndex];
            currentNode.Run();

            State nodeState = currentNode.GetState();

            if (nodeState != State.Failure)
            {
                myState = nodeState;
            }
            else
            {
                nodeIndex++;

                if (nodeIndex >= numNodes)
                {
                    myState = State.Success;
                    Reset();
                }
                else
                {
                    myState = State.Running;
                }
            }
        }

        public override State GetState()
        {
            return myState;
        }

        public override void Reset()
        {
            nodeIndex = 0;
        }
    }
}
