using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Cut;
using LevelManagement;

namespace Supplies
{
    public class TongueSpawner : MonoBehaviour
    {
        public float spawnInterval = 10f;
        public SupplyArea tongueArea;
        public Rigidbody attachPoint;
        public GameObject spawnPoint;

        private SupplyPoint supplyPoint;
        private float timeSinceLastSpawn = 0f;

        // Use this for initialization
        void Start()
        {
            foreach (GameObject spawnPoint in tongueArea.spawnPoints)
            {
                Vector3 position = spawnPoint.transform.position;
                Quaternion rotation = spawnPoint.transform.rotation;

                supplyPoint = new SupplyPoint(tongueArea.objectToSpawn, position, rotation);
            }
        }

        // Update is called once per frame
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


        void CheckSpawnPoints()
        {
            GameObject objectToSpawn = supplyPoint.objectToSpawn;

            Vector3 position = spawnPoint.transform.position;
            Quaternion rotation = gameObject.transform.rotation;
            float radius = 0.15f;

            // Get names of all objects near the point
            List<string> objectsAtPoint = new List<string>();
            Collider[] colliders = Physics.OverlapSphere(position, radius);
            foreach(Collider c in colliders)
            {
                string item = c.gameObject.name;
                if (!objectsAtPoint.Contains(item))
                {
                    objectsAtPoint.Add(item);
                }
            }

            // Spawn and Attach
            if (!ListContainsName(objectsAtPoint, objectToSpawn.gameObject.name))
            {
                GameObject tongue = Instantiate(objectToSpawn, position, rotation);
                tongue.GetComponent<SpringJoint>().connectedBody = attachPoint;
            }
        }

        bool ListContainsName(List<String> list, string name)
        {
            return list.Contains(name) || list.Contains(name + "(Clone)");
        }
    }
}