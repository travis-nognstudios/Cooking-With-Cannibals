using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour
{
    public GameObject switchObj;
	public GameObject AffectedObj;
	public GameObject AffectedObj1;

    private Animator anim;
    private Animator anim1;
    private bool isTriggered = false;
    public bool multipleObj = false;
    public bool forSky = false;
    public bool mainmenu = false;


    void Start()
    {

        if (forSky == false)
        {
            anim = AffectedObj.GetComponent<Animator>();
            if (multipleObj == true)
            {
                anim1 = AffectedObj1.GetComponent<Animator>();
            }
        }
            

            AnimateOn(this.gameObject);

    }
  
	
	private void OnTriggerEnter(Collider col)
	{
      
        if (!isTriggered){
            AnimateOn(switchObj);
			switchObj.gameObject.SetActive(true); 
			this.gameObject.SetActive(false); 
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
            if (forSky == true)
            {
                AffectedObj.gameObject.SetActive(false);
                AffectedObj1.gameObject.SetActive(true);

            }
            else if (mainmenu == true)
            {
                AffectedObj.gameObject.SetActive(true);
            }
            else
            {
                anim.enabled = true;
                if (multipleObj == true)
                {
                    anim1.enabled = true;
                }
            }
        }
        if (obj.name.Contains("_off"))
        {
            if (forSky == true)
            {
                AffectedObj.gameObject.SetActive(true);
                AffectedObj1.gameObject.SetActive(false);

            }
            else if (mainmenu == true)
            {
                AffectedObj.gameObject.SetActive(false);
            }
            else
            {
                anim.enabled = false;
                if (multipleObj == true)
                {
                    anim1.enabled = false;
                }
            }
        }
    }
}
