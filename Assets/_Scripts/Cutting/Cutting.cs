using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cutting
{
    public class Cutting : MonoBehaviour
    {
        public Material capMaterial;

        private void OnTriggerEnter(Collider collision)
        {   
            GameObject victim = collision.gameObject;

            GameObject[] pieces = MeshCut.Cut(victim, transform.position, transform.right, capMaterial);

            if (!pieces[1].GetComponent<Rigidbody>())
                pieces[1].AddComponent<Rigidbody>();

            Destroy(pieces[1], 1);
        }
    }
}