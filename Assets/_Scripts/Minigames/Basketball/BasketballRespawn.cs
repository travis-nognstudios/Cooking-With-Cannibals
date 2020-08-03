using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketballRespawn : MonoBehaviour
{

    public GameObject originalPos;
    public GameObject spawnObject;
    public float respawnDelay;

    public float despawnDelay;

    private int ballsSpawned = 0;
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Respawn"))
        {
            other.tag = "Untagged";
            StartCoroutine(SpawnBall(respawnDelay));
            StartCoroutine(DestroyAfter(other, despawnDelay));
        }
    }

    private IEnumerator SpawnBall(float respawnDelay)
    {
        //ballsSpawned++;
        yield return new WaitForSeconds(respawnDelay);

        //for (int i = 0; i < ballsSpawned; i++)
        //{
            Instantiate(spawnObject, originalPos.transform.position, Quaternion.identity);
        //}
    }

    private IEnumerator DestroyAfter(Collider other, float despawnDelay)
    {
        yield return new WaitForSeconds(despawnDelay);
        Destroy(other.gameObject);
    }
}
