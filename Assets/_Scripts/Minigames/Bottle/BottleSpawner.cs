using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleSpawner : MonoBehaviour
{
    public GameObject bottles;
    public GameObject spawnPoint;
    public void Spawn()
    {
        Instantiate(bottles, spawnPoint.transform.position, Quaternion.identity);
    }
}
