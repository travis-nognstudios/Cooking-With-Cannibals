using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Supplies
{
    public class SupplyManagerv2 : MonoBehaviour
    {
        public float spawnInterval = 10f;
        public SupplyArea[] ingredients;

        private float timeSinceLastSpawn = 0f;

        // Use this for initialization
        void Start()
        {
            CheckSpawnPoints();
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
            foreach (SupplyArea ingredientArea in ingredients)
            {
                GameObject item = ingredientArea.objectToSpawn;
                string ingredientName = item.gameObject.name;

                foreach (GameObject spawnpoint in ingredientArea.spawnPoints)
                {
                    // Get names of all objects near spawn point
                    // If it doesn't contain the designated object, instantiate it
                    
                    Vector3 spawnposition = spawnpoint.transform.position;
                    float radius = 0.1f;

                    List<string> objectsAtPoint = new List<string>();

                    Collider[] colliders = Physics.OverlapSphere(spawnposition, radius);
                    foreach(Collider c in colliders)
                    {
                        string objName = c.gameObject.name;
                        if (!objectsAtPoint.Contains(objName))
                        {
                            objectsAtPoint.Add(objName);
                        }
                    }

                    if (!ListContainsName(objectsAtPoint, ingredientName))
                    {
                        Instantiate(item, spawnposition, item.transform.rotation);
                    }
                }
            }
        }

        bool ListContainsName(List<string> l, string n)
        {
            foreach (string listname in l)
            {
                if (listname.Contains(n))
                {
                    return true;
                }
            }

            return false;
        }
    }
}