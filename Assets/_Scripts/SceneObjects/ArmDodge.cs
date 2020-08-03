using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmDodge : MonoBehaviour
{
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        //rb = gameObject.GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Knife"))
        {
            Debug.Log(other.transform.parent.GetComponent<Rigidbody>().velocity.magnitude);
            //if (other.transform.parent.GetComponent<Rigidbody>().velocity.magnitude > 1f)
            //{
               // Debug.Log("ArmDodge.cs: dodge" + rb);
                rb.AddForce(10f, 0, 0, ForceMode.Impulse);
           // }
            
        }
    }
}
