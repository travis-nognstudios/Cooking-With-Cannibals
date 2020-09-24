using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidingWith : MonoBehaviour
{
    public void OnCollisionEnter(Collision obj)
    {
        Debug.Log("Colliding with:  " + obj.gameObject);
    }
}
