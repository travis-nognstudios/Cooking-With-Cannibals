using System;
using UnityEngine;


namespace Supplies
{
    [System.Serializable]
    public struct SupplyPoint
    {
        public GameObject objectToSpawn;
        public Vector3 point;
        public Quaternion rotation;

        public SupplyPoint(GameObject objectToSpawn, Vector3 point, Quaternion rotation)
        {
            this.objectToSpawn = objectToSpawn;
            this.point = point;
            this.rotation = rotation;
        }
    }
}