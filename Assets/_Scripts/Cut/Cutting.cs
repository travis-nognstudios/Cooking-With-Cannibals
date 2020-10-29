using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Cutting : MonoBehaviour
{
    [SerializeField]
    private GameObject[] choppedObject;
    [SerializeField, Tooltip("Amount of slices for object to get cut")]
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
    public Camera cam;

    void Start()
    {
        progressSlider.maxValue = cuttingChops;
        numberChops = 0;
        chopped = false;
        canChop = true; 
    }

    void Update()
    {
        progressBar.transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
        if (chopped)
        {
            chopped = false;
            canChop = false;
            progressBar.SetActive(false);
            GameObject newChoppedObj = Instantiate(choppedObject[0], transform.position, Quaternion.identity);
            newChoppedObj.name = choppedObject[0].name;
            for (int i = 1; i < choppedObject.Length; i++)
            {
                InstantiateNextTo(newChoppedObj, choppedObject[i]);
            }
            

            Destroy(gameObject);
            //Instantiate(choppedObject, transform.position, transform.rotation);
        }

    }
    void InstantiateNextTo(GameObject orig, GameObject prefab)
    {
        Transform t = orig.transform;
        Mesh m = orig.GetComponent<MeshFilter>().sharedMesh;
        // spawn prefab object next to original (in the x direction)
        float x = m.bounds.size.x * t.localScale.x;
        Instantiate(prefab, t.position + new Vector3(x, 0, 0), Quaternion.identity);
    }
    public void Chop()
    {
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
