using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour
{
    public GameObject switchObj;
	public GameObject AffectedObj;
	//public Spin script;
	//private string objName;
	//private string scriptName;
    private bool isTriggered = false;
	
	void Start()
	{
		//objName = AffectedObj.gameObject.name;
		//scriptName = script.gameObject.name;
	}
	
	private void OnTriggerEnter(Collider col)
	{
		if (this.gameObject.name.Contains("_on")){
			//AffectedObj.GetComponent(Spin).setActive(false);//enabled = false; //
			//GameObject.Find(AffectedObj.GameObject.name).GetComponent<script.gameObject.name>().setActive(false);
			//GameObject.Find(ObjName).GetComponent(script).setActive(false);
			Debug.Log("name contains _on");
        }
		else if (this.gameObject.name.Contains("_off")){
			Debug.Log("name contains _off");
			//AffectedObj.GetComponent(Spin).setActive(true);//enabled = true; //
			//GameObject.Find(AffectedObj.GameObject.name).GetComponent<script.gameObject.name>().setActive(true);
			//GameObject.Find(ObjName).GetComponent(script).setActive(true);
        }
		else
		{
			Debug.Log("name contains _off");
		}

		if(!isTriggered){
			switchObj.gameObject.SetActive(true); //gameObject.enable();//
			this.gameObject.SetActive(false); //gameObject.disable();//
			isTriggered = true;
			Debug.Log("Colliding gameobject: " + col.gameObject.name);
			Debug.Log("isTriggered = true");
		}
	}
	private void OnTriggerExit(Collider col)
    {
        if (isTriggered)
            isTriggered = false;
		Debug.Log("isTriggered = false");
    }
}
