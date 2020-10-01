using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEnabler : MonoBehaviour 
{
    public ParticleSystem bits;

    private void OnCollisionEnter(Collision collision)
    {
        bits.GetComponent<ParticleSystem>().Play();
    }
}
