using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Postit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //this.GetComponent<Rigidbody>().useGravity = true;
        //this.GetComponent<Rigidbody>().isKinematic = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  
    void OnCollisionEnter(Collision c)
    {
        //var joint = gameObject.AddComponent<FixedJoint>();
        //joint.connectedBody = c.rigidbody;
        Debug.Log(c.gameObject.name);
        this.GetComponent<Rigidbody>().useGravity = false;
        //this.GetComponent<Rigidbody>().isKinematic = true;
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    private void OnCollisionExit(Collision collision)
    {
        this.GetComponent<Rigidbody>().useGravity = true;
        //this.GetComponent<Rigidbody>().isKinematic = false;
        Debug.Log("gravity on");
    }

}
