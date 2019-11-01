 using UnityEngine;
using System.Collections;

public class Cutter : MonoBehaviour {

	public Material capMaterial;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cookable"))
        {
            GameObject victim = collision.collider.gameObject;

            GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut(victim, transform.position, transform.right, capMaterial);

            if (!pieces[1].GetComponent<Rigidbody>())
                pieces[1].AddComponent<Rigidbody>();

            Destroy(pieces[1], 1);
        }
    }

}
