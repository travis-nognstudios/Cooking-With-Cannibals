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
            for (int i = 0; i < supplyPoints.Length; i++)
            {
                supplyPoints[i].numberSpawned = 0;
            }
        }
        // Update is called once per frame
        void Update()
        {
            sinceLastSpawn += Time.deltaTime;
            foreach (SupplyPoint supplyPoint in supplyPoints)
            {
                if (sinceLastSpawn >= spawnInterval && supplyPoint.maxSpawnNumber > supplyPoint.numberSpawned)
                {
                    sinceLastSpawn = 0;
                    SpawnItems(supplyPoint);
                    
                }
            }
            

        }

        void SpawnItems(SupplyPoint supplyPoint)
        {
            supplyPoint.numberSpawned++;
            Debug.Log(supplyPoint.numberSpawned);
            Instantiate(supplyPoint.objectToSpawn, supplyPoint.spawnArea.transform.position, supplyPoint.spawnArea.transform.rotation);

            Debug.Log("Spawned " + supplyPoint.objectToSpawn.name);
            
        }
    }
}