using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropDestruction : MonoBehaviour
{
    public GameObject breakVersion;
    public float bForce = 1f;
    protected Rigidbody rb;
    private int active = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(rb.velocity.magnitude > bForce && active==0)
        {
            active++;
            Instantiate(breakVersion, transform.position, new Quaternion(0,90,90,0));
            //explosion for geo breakables
            //rb.AddExplosionForce(10f, Vector3.zero, 0f);
            Destroy(gameObject);
        }
    }
}
