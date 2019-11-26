using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
    [SerializeField]
    private Slider progressSlider;
    [SerializeField]
    private GameObject progressBar;

    private void Start()
    {
        progressSlider.maxValue = cuttingChops;
        numberChops = 0;
        chopped = false;
        canChop = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckChopped();
        if (chopped)
        {
            progressBar.SetActive(false);
            for (int i = 0; i < choppedObject.Length; i++)
            {
                GameObject newChoppedObj = Instantiate(choppedObject[i], transform.position, transform.rotation);
                newChoppedObj.name = choppedObject[i].name;
            }
            

            Destroy(gameObject);
            //Instantiate(choppedObject, transform.position, transform.rotation);
        }

    }
    public void Chop()
    {
        progressBar.SetActive(true);
        numberChops++;
        progressSlider.value = numberChops;
    }

    private void OnTriggerStay(Collider other)
    {
        canChop = false;
    }
    private void OnTriggerExit(Collider other)
    {
        canChop = true;
    }
    public void TryChop()
    {
        if (canChop == true)
        {
            Chop();
        }
    }
    public bool CheckChopped()
    {
        canChop = false;
        if (numberChops >= cuttingChops)
        {
          chopped = true;
        }
        return chopped;
    }


}
