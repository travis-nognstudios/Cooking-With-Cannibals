using System;
using UnityEngine;


namespace Supplies
{
    [System.Serializable]
    public struct SupplyPoint
    {
        public GameObject objectToSpawn;
        public Vector3 point;

        public SupplyPoint(GameObject objectToSpawn, Vector3 point)
        {
            this.objectToSpawn = objectToSpawn;
            this.point = point;
        }
    }
}