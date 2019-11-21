using System;
using UnityEngine;


namespace Supplies
{
    [System.Serializable]
    public struct SupplyPoint
    {
        public GameObject objectToSpawn;
        public GameObject spawnArea;
        public int maxSpawnNumber;
        [HideInInspector]
        public int numberSpawned;
    }
}