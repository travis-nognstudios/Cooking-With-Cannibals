using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cutting
{
    public class Cutting : MonoBehaviour
    {

        public Material CapMaterial;


        private void OnTriggerEnter(Collision collision)
        {
            GameObject victim = collision.collider.gameObject;

            GameObject[] pieces = MeshCut.Cut(victim, transform.position, transform.right, CapMaterial);

            if (!pieces[1].GetComponent<Rigidbody>())
                pieces[1].AddComponent<Rigidbody>();

            Destroy(pieces[1], 1);
        }
    }
}