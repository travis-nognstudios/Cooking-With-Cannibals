using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingRespawn : MonoBehaviour
{

    public GameObject originalPos;
    public GameObject spawnObject;
    [HideInInspector]
    public int ballsSpawned = 0;
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Respawn"))
        {
            other.tag = "Untagged";
            
            StartCoroutine("SpawnBall");
        }
    }

    private IEnumerator SpawnBall()
    {
        yield return new WaitForSeconds(3f);
        Instantiate(spawnObject, originalPos.transform.position, Quaternion.identity);
        ballsSpawned++;
    }
}
