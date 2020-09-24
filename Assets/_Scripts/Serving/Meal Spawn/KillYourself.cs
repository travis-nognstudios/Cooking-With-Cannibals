using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillYourself : MonoBehaviour
{
    public float time = 5f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, time);
    }

}
