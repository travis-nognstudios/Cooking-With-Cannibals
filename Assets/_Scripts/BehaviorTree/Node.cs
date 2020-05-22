using System;
using UnityEngine;

namespace BehaviorTree
{
    abstract class Node : MonoBehaviour
    {
        public abstract void Run();
        public abstract State GetState();
    }
}
