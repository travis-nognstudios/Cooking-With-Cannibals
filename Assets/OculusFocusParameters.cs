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
            Debug.Log("The player should be in game and accepting input");
            rHand.GetComponent<Renderer>().enabled = true;
            lHand.GetComponent<Renderer>().enabled = true;
        }

       if (!OVRManager.hasInputFocus)
        {
            Debug.Log("The player hit the dash button. No input should be processing at this time");
            rHand.GetComponent<Renderer>().enabled = false;
            lHand.GetComponent<Renderer>().enabled = false;
        }
    }
}
