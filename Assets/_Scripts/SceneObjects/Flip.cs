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
    private Animator anim;
    private bool isTriggered = false;

    void Start()
    {
        //objName = AffectedObj.gameObject.name;
        //scriptName = script.gameObject.name;

        anim = AffectedObj.GetComponent<Animator>();
        AnimateOn(this.gameObject);
        

    }
  
	
	private void OnTriggerEnter(Collider col)
	{
        /*
		if (this.gameObject.name.Contains("_on")){
            //AffectedObj.GetComponent(Spin).setActive(false);//enabled = false; //
            //GameObject.Find(AffectedObj.GameObject.name).GetComponent<script.gameObject.name>().setActive(false);
            //GameObject.Find(ObjName).GetComponent(script).setActive(false);

            anim.enabled = false;
			Debug.Log("name contains _on");
        }
        /*
		else if (this.gameObject.name.Contains("_off")){
			Debug.Log("name contains _off");
            //AffectedObj.GetComponent(Spin).setActive(true);//enabled = true; //
            //GameObject.Find(AffectedObj.GameObject.name).GetComponent<script.gameObject.name>().setActive(true);
            //GameObject.Find(ObjName).GetComponent(script).setActive(true);

            anim.enabled = true;
        }
        //;
		else
		{
            anim.enabled = true;
			Debug.Log("name contains _off");
		}
        */





        if (!isTriggered){
            AnimateOn(switchObj);
			switchObj.gameObject.SetActive(true); //gameObject.enable();//
			this.gameObject.SetActive(false); //gameObject.disable();//
			isTriggered = true;

        }
	}
	private void OnTriggerExit(Collider col)
    {
        if (isTriggered)
        {
            isTriggered = false;
        }

    }
    
    public void AnimateOn(GameObject obj)
    {
        if (obj.name.Contains("_on"))
        {
            anim.enabled = true;
            Debug.Log("name contains _on");
        }
        if (obj.name.Contains("_off"))
        {
            anim.enabled = false;
            Debug.Log("name contains _off");
        }
    }
}
