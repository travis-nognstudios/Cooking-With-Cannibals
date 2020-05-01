using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleSpawner : MonoBehaviour
{
    public GameObject bottles;
    public void Spawn()
    {
        Instantiate(bottles, this.transform);
    }
}
