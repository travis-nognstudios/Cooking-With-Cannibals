using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRespawn : MonoBehaviour
{
    #region Variables

    private Vector3 startingPosition;
    private Vector3 connectedstartingPosition;

    private Quaternion startingRotation;
    private Quaternion connectedstartingRotation;

    private Rigidbody rb;
    public Rigidbody connected;
    #endregion

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();

        startingPosition = this.transform.position;
        startingRotation = this.transform.rotation;

        if (connected != null)
        {
            connectedstartingPosition = connected.transform.position;
            connectedstartingRotation = connected.transform.rotation;
        }
       
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("destroy"))
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        this.transform.position = startingPosition;
        this.transform.rotation = startingRotation;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        if (connected != null)
        {
            connected.transform.position = connectedstartingPosition;
            connected.transform.rotation = connectedstartingRotation;

            connected.velocity = Vector3.zero;
            connected.angularVelocity = Vector3.zero;
        }
    }
}

