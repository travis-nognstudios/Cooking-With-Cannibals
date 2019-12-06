using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public bool isOn = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isOn)
        {
            isOn = true;
            transform.position = new Vector3(-0.4937657f, -0.1230854f, 0);
            transform.Rotate(0, 0, -21.76f, Space.Self);
        }
        else
        {
            transform.position = new Vector3(0, 0, 0);
            transform.Rotate(0, 0, 21.76f, Space.Self);
        }
    }
}
