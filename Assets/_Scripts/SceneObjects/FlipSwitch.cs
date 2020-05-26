using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipSwitch : MonoBehaviour
{
	public GameObject switchOn;
	public GameObject switchOff;
	private bool isOn = false;
	//bool triggered = false
    
	void Start () {
		switchOff.SetActive(true); 
		switchOn.SetActive(false);
    }
	private void OnTriggerEnter(Collider other)
	{
		// if (!isTriggered)
        //{
        //    isTriggered = true;
			if (!isOn)
			{
				switchOff.SetActive(false); //gameObject.disable();
				switchOn.SetActive(true); //gameObject.enable();
				isOn = true;
			}
			else
			{
				switchOn.gameObject.SetActive(false); //disable();
				switchOff.gameObject.SetActive(true); //enable();
				isOn = false;
			}
		//}
	}
	/*
	private void OnTriggerExit(Collider other)
    {
        if (isTriggered)
            isTriggered = false;
    }
	*/
}
