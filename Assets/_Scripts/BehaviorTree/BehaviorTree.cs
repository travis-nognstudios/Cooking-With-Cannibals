using System;
using UnityEngine;

namespace BehaviorTree
{
    class BehaviorTree : MonoBehaviour
    {
        public Composite root;

        void Update()
        {
            root.Run();

            State currentState = root.GetState();
            if (currentState == State.Success)
            {
                root.Reset();
            }
        }
    }
}
