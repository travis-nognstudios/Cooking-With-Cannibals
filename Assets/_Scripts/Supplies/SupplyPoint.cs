using System;
using UnityEngine;


namespace Supplies
{
    [System.Serializable]
    public struct SupplyPoint
    {
        public GameObject object_to_spawn;
        public GameObject spawnArea;
        public int maxSpawnNumber;
    }
}