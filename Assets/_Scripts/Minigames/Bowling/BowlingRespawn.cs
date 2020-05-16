using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingRespawn : MonoBehaviour
{

    public GameObject originalPos;
    public GameObject spawnObject;
    [HideInInspector]
    public int ballsSpawned = 0;

    public float respawnDelay;
    public float despawnDelay;
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Respawn"))
        {
            other.tag = "Untagged";
            
            StartCoroutine("SpawnBall");
            StartCoroutine(DestroyAfter(other, despawnDelay));
        }
    }

    private IEnumerator SpawnBall()
    {
        yield return new WaitForSeconds(3f);
        Instantiate(spawnObject, originalPos.transform.position, Quaternion.identity);
        ballsSpawned++;
    }

    private IEnumerator DestroyAfter(Collider other, float despawnDelay)
    {
        yield return new WaitForSeconds(despawnDelay);
        Destroy(other.gameObject);
    }
}
