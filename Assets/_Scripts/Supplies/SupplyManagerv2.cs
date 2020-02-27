﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Cut;
using LevelManagement;

namespace Supplies
{
    public class SupplyManagerv2 : MonoBehaviour
    {
        public float spawnInterval = 10f;
        public SupplyArea[] ingredients;

        // Camera is used by the chopping UI
        public Camera CenterEyeCamera;

        private List<SupplyPoint> supplyPoints = new List<SupplyPoint>();
        private float timeSinceLastSpawn = 0f;

        // Use this for initialization
        void Start()
        {
            // Get starting positions for all ingredients
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

                // If desired object doesn't exist near point, spawn new one
                // Set its camera on the Cutting script to the center eye camera, if cuttable
                if (!ListContainsName(objectsAtPoint, objectToSpawn.gameObject.name))
                {
                    GameObject spawnedItem = Instantiate(objectToSpawn, position, rotation);
                    Cuttable cutScript = spawnedItem.GetComponent<Cuttable>();

                    if (cutScript != null)
                    {
                        cutScript.cam = CenterEyeCamera;
                    }
                }
            }
        }

        bool ListContainsName(List<String> list, string name)
        {
            return list.Contains(name) || list.Contains(name + "(Clone)");
        }
    }
}