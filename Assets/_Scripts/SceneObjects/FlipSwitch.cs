using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipSwitch : MonoBehaviour
{
	public GameObject switchOn;
	public GameObject switchOff;
	private bool isOn = false;

    
	void Start () {
		switchOff.SetActive(true); 
		switchOn.SetActive(false);
    }
	private void OnTriggerEnter(Collider other)
	{
		
			if (!isOn)
			{
				switchOff.SetActive(false); 
				switchOn.SetActive(true); 
				isOn = true;
			}
			else
			{
				switchOn.gameObject.SetActive(false); 
				switchOff.gameObject.SetActive(true); 
				isOn = false;
			}
	
	}

}
