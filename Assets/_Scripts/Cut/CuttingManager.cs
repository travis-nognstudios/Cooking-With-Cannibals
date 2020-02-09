using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingManager : MonoBehaviour
{

    [SerializeField]
    PreCutMesh[] preCutMeshes;
    // Start is called before the first frame update

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Knife collision");
        for (int i = 0; i < preCutMeshes.Length; i++)
        {
            if (collision.collider == preCutMeshes[i].whole)
            {
                Destroy(preCutMeshes[i].whole);
                Instantiate(preCutMeshes[i].cut, preCutMeshes[i].whole.transform);

            }
        }
                
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cuttable"))
        {
        }
    }
}
