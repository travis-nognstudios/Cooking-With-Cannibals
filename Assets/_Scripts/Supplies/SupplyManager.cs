using UnityEngine;
using System.Collections;

namespace Supplies
{
    public class SupplyManager : MonoBehaviour
    {
        public int spawnInterval = 5;
        public SupplyPoint[] supplyPoints;

        private float sinceLastSpawn = 0;

        // Use this for initialization
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            sinceLastSpawn += Time.deltaTime;

            if (sinceLastSpawn >= spawnInterval)
            {
                sinceLastSpawn = 0;
                SpawnItems();
            }

        }

        void SpawnItems()
        {
            foreach(SupplyPoint point in supplyPoints)
            {
                // ToDo: Check how many are already in supplyarea
                GameObject item = point.object_to_spawn;
                Vector3 pos = point.spawnArea.transform.position;

                GameObject clone = Instantiate(item);
                clone.transform.position = pos;
                Debug.Log("Spawned " + item.name);
            }
        }
    }
}