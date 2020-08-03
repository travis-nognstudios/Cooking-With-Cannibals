using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shock : MonoBehaviour
{
    public ParticleSystem shock;
    void Start()
    {
        shock.Stop();
    }

    public void OnCollisionEnter(Collision collision)
    {
        shock.Play();
    }

    public void OnCollisionExit(Collision collision)
    {
        shock.Stop();
    }

}
