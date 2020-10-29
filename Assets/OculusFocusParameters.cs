using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OculusFocusParameters : MonoBehaviour
{
    public GameObject rHand;
    public GameObject lHand;

    private void Start()
    {
        rHand = GameObject.Find("hand_right_renderPart_0");

        lHand = GameObject.Find("hand_left_renderPart_0");
    }

    void Update()
    {
       if (OVRManager.hasInputFocus)
        {
            rHand.GetComponent<Renderer>().enabled = true;
            lHand.GetComponent<Renderer>().enabled = true;
        }

       if (!OVRManager.hasInputFocus)
        {
            rHand.GetComponent<Renderer>().enabled = false;
            lHand.GetComponent<Renderer>().enabled = false;
        }
    }
}
