using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMaterial : MonoBehaviour
{
    public Material mat;
    private GameObject rightHand;
    private GameObject leftHand;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rightHand = GameObject.Find("hand_right_renderPart_0");
        leftHand = GameObject.Find("hand_left_renderPart_0");

        if (rightHand != null && leftHand != null)
        {
            leftHand.GetComponent<Renderer>().material = mat;
            rightHand.GetComponent<Renderer>().material = mat;

            Destroy(GetComponent<HandMaterial>());
        }
    }
}
