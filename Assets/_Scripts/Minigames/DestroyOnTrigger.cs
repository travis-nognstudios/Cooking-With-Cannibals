using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTrigger : MonoBehaviour
{
    public float despawnTime;
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(DestroyRoutine(other, despawnTime));
    }

    private IEnumerator DestroyRoutine(Collider other, float despawnTime)
    {
        yield return new WaitForSeconds(3f);
        Destroy(other);
    }
}
