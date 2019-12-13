using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

                    SupplyPoint point = new SupplyPoint(area.objectToSpawn, position);
                    supplyPoints.Add(point);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            timeSinceLastSpawn += Time.deltaTime;

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
                Quaternion rotation = objectToSpawn.transform.rotation;
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
                // Set its camera on the Cutting script to the center eye camera
                if (!ListContainsName(objectsAtPoint, objectToSpawn.gameObject.name))
                {
                    GameObject spawnedItem = Instantiate(objectToSpawn, position, rotation);
                    spawnedItem.GetComponent<Cutting>().cam = CenterEyeCamera;
                }
            }
        }

        bool ListContainsName(List<string> list, string name)
        {
            foreach (string listitem in list)
            {
                // Check if one string is a substring of another
                // Allows them to have numbers at the end and still match
                if (listitem.Contains(name) || name.Contains(listitem))
                {
                    return true;
                }
            }

            return false;
        }
    }
}