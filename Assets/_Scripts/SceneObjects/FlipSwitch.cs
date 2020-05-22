using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipSwitch : MonoBehaviour
{
	public GameObject switchOn;
	public GameObject switchOff;
	private bool on;
    // Start is called before the first frame update
    void Start()
    {
        on = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	private void OnTriggerEnter(Collider col)
	{
		if (!on)
		{
			switchOff.SetActive(false); //gameObject.disable();
			switchOn.SetActive(true); //gameObject.enable();
			on = true;
		}
		else
		{
			switchOn.gameObject.SetActive(false); //disable();
			switchOff.gameObject.SetActive(true); //enable();
			on = false;
		}
	}
}
