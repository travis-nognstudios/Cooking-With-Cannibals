using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxClose : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Styrofoam_Bottom")
        {
            transform.parent.GetComponent<BoxParent>().BoxClosed(this);
            Debug.Log("Box Closed");
            
        }
    }
}
