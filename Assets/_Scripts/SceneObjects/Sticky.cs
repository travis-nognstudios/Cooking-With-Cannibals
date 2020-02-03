using UnityEngine;
using System.Collections;

public class Sticky : MonoBehaviour
{
    public Collider stickTo;
    public float stickTime;

    private Rigidbody rb;

    private bool stuck;
    private float timer;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stuck)
        {
            timer += Time.deltaTime;
            if (timer >= stickTime)
            {
                UnStick();
            }
        }
    }

    void OnCollisionEnter(Collider other)
    {
        if (other == stickTo)
        {
            Stick();
        }
    }

    void Stick()
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
        stuck = true;
    }

    void UnStick()
    {
        rb.constraints = RigidbodyConstraints.None;
        stuck = false;
    }
}
