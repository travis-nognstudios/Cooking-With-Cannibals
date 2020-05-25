using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipSwitch : MonoBehaviour
{
	public GameObject switchOn;
	public GameObject switchOff;
	private bool isOn = false;
    
	private void OnTriggerEnter(Collider col)
	{
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
	}
}
