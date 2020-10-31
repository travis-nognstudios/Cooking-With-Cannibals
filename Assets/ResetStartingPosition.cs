using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStartingPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        OVRManager.display.RecenterPose();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
