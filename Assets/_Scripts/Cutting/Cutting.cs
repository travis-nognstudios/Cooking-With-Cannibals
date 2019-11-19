using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cutting : MonoBehaviour
{
    [SerializeField]
    private GameObject choppedObject;
    [SerializeField, Tooltip("Amount of slices for object to become cut")]
    private int cuttingChops;
    [HideInInspector]
    private int numberChops;
    private bool chopped;


    private void Start()
    {
        numberChops = 0;
        chopped = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        checkChopped();
        if (chopped)
        {
            Instantiate(choppedObject, transform.position, transform.rotation);
            Destroy(gameObject);
            //Instantiate(choppedObject, transform.position, transform.rotation);
        }
    }
    public void Chop()
    {
        Debug.Log(chopped);
        Debug.Log(numberChops);
        numberChops++;
    }

    public bool checkChopped()
    {
        if (numberChops >= cuttingChops)
        {
          chopped = true;
        }
        return chopped;
    }


}
