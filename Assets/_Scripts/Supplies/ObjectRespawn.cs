using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRespawn : MonoBehaviour
{
    #region Variables

    private Vector3 startingPosition;
    private Quaternion startingRotation;
    private Rigidbody rb;
    public Rigidbody connected;
    #endregion

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        startingPosition = this.transform.position;
        startingRotation = this.transform.rotation;
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "destroy")
        {
            this.transform.position = startingPosition;
            this.transform.rotation = startingRotation;

            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            if (connected!= null)
            {
                connected.velocity = Vector3.zero;
                connected.angularVelocity = Vector3.zero;
            }
        }
    }
}

