using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cutting : MonoBehaviour
{
    [SerializeField]
    private GameObject[] choppedObject;
    [SerializeField, Tooltip("Amount of slices for object to become cut")]
    private int cuttingChops;
    [HideInInspector]
    private int numberChops;
    private bool chopped;
    [HideInInspector]
    public bool canChop;


    private void Start()
    {
        numberChops = 0;
        chopped = false;
        canChop = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        checkChopped();
        if (chopped && canChop == false)
        {
            foreach (GameObject choppedObj in choppedObject)
            {
                GameObject newChoppedObj = Instantiate(choppedObj, transform.position, transform.rotation);
                newChoppedObj.name = choppedObj.name;
            }

            Destroy(gameObject);
            //Instantiate(choppedObject, transform.position, transform.rotation);
        }
        else 
        {
            canChop = true;
        }
    }
    public void Chop()
    {
        numberChops++;
    }

    public bool checkChopped()
    {
        canChop = false;
        if (numberChops >= cuttingChops)
        {
          chopped = true;
        }
        return chopped;
    }


}
