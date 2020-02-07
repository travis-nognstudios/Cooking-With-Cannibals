using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public bool isOn = false;
    bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggered)
        {
            isTriggered = true;
            if (!isOn)
            {
                isOn = true;
                transform.position = new Vector3(-0.4937657f, -0.1230854f, 0);
                transform.Rotate(0, 0, -21.76f, Space.Self);

                LevelManager.Instance.LoadNextScene();
            }
            else
            {
                isOn = false;
                transform.position = new Vector3(0, 0, 0);
                transform.Rotate(0, 0, 21.76f, Space.Self);
            }
        }   
    }
    private void OnTriggerExit(Collider other)
    {
        if (isTriggered)
            isTriggered = false;
    }
}
