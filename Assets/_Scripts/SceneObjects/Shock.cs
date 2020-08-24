using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shock : MonoBehaviour
{
    public ParticleSystem shock;
    public ParticleSystem shock1;
    public ParticleSystem shock2;
    void Start()
    {
        shock.Stop();
        shock1.Stop();
        shock2.Stop();
    }

    public void OnCollisionEnter(Collision collision)
    {
        shock.Play();
        shock1.Play();
        shock2.Play();
    }

    public void OnCollisionExit(Collision collision)
    {
        shock.Stop();
        shock1.Stop();
        shock2.Stop();
    }

}
