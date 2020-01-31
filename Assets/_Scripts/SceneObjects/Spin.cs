using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public int x = 0;
    public int y = 0;
    public int z = 0;

    void Update()
    {
        transform.Rotate(x, y, z);
    }
}
