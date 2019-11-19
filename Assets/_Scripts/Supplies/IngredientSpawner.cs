using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    public int spawnInterval = 5;
    public GameObject objectToSpawn;
    //public GameObject spawnArea;
    public int maxSpawnNumber;
    [HideInInspector]
    private int numberSpawned;
    

    private float sinceLastSpawn = 0;
    // Start is called before the first frame update
    void Start()
    {
        numberSpawned = 0;
    }

    // Update is called once per frame
    void Update()
    {
        sinceLastSpawn += Time.deltaTime;
        if (sinceLastSpawn >= spawnInterval && maxSpawnNumber > numberSpawned)
        {
            sinceLastSpawn = 0;
            SpawnItem();

        }
    }

    void SpawnItem()
    {
        numberSpawned++;
        
        Instantiate(objectToSpawn, transform.position, transform.rotation);

        Debug.Log("Spawned " + objectToSpawn.name);

    }
}
