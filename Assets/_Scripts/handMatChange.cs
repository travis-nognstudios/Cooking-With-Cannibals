using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handMatChange : MonoBehaviour
{
    // assign this script to the OVR player
    public Material mat; // this is the material you want the hands changed too. drag and drop it in the inspector

    private GameObject handLeft;
    private GameObject handRight;




    //
    void Update()
    {
        // grab the hands based on their name
        handRight = GameObject.Find("hand_right_renderPart_0");
        handLeft = GameObject.Find("hand_left_renderPart_0");

        // assuming we've found the hands change the texture
        if (handRight != null && handLeft != null)
        {
            handLeft.GetComponent<Renderer>().material = mat;
            handRight.GetComponent<Renderer>().material = mat;
            Destroy(GetComponent<handMatChange>()); // remove this script so it stops running
        }
    }
}