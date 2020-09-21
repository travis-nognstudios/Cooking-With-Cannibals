using UnityEngine;
using System;
using System.Collections.Generic;
using LevelManagement;

namespace Supplies
{
    public class SupplyManagerv2 : MonoBehaviour
    {
        public float spawnInterval = 10f;
        public SupplyArea[] ingredients;

        private List<SupplyPoint> supplyPoints = new List<SupplyPoint>();
        private float timeSinceLastSpawn = 0f;

        void Start()
        {
            GetStartingPositionForAllIngredients();
        }

        void Update()
        {
            timeSinceLastSpawn += PauseTimer.DeltaTime();

            // At every interval, check spawn points
            if (timeSinceLastSpawn >= spawnInterval)
            {
                timeSinceLastSpawn = 0f;
                CheckSpawnPoints();
            }
        }

        void GetStartingPositionForAllIngredients()
        {
            foreach (SupplyArea area in ingredients)
            {
                foreach (GameObject spawnPoint in area.spawnPoints)
                {
                    Vector3 position = spawnPoint.transform.position;
                    Quaternion rotation = spawnPoint.transform.rotation;

                    SupplyPoint point = new SupplyPoint(area.objectToSpawn, position, rotation);
                    supplyPoints.Add(point);
                }
            }
        }

        void CheckSpawnPoints()
        {
            foreach (SupplyPoint point in supplyPoints)
            {
                GameObject objectToSpawn = point.objectToSpawn;
                Vector3 position = point.point;
                Quaternion rotation = point.rotation;
                float radius = 0.1f;

                // Get names of all objects near the point
                List<string> objectsAtPoint = new List<string>();
                Collider[] colliders = Physics.OverlapSphere(point.point, radius);
                foreach(Collider c in colliders)
                {
                    string item = c.gameObject.name;
                    if (!objectsAtPoint.Contains(item))
                    {
                        objectsAtPoint.Add(item);
                    }
                }

                if (!ListContainsName(objectsAtPoint, objectToSpawn.gameObject.name))
                {
                    Instantiate(objectToSpawn, position, rotation);
                }
            }
        }

        bool ListContainsName(List<String> list, string name)
        {
            return list.Contains(name) || list.Contains(name + "(Clone)");
        }
    }
}