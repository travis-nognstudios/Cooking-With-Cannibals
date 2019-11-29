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
        //Debug.Log(numberChops);
    }

    //private void OnTriggerEnter(Collider other)
    private void Update()
    {
        
        if (chopped)
        {
            chopped = false;
            canChop = false;
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
        //Debug.Log(numberChops);
        progressBar.SetActive(true);
        numberChops++;
        progressSlider.value = numberChops;
        CheckChopped();
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
