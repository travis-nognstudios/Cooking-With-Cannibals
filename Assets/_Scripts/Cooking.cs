using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooking : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private Material cooked;

    //[SerializeField]
    private Material burnt;
    
    [SerializeField]
    private Renderer rend;

    private float timeCooked;

    #endregion Variables

    // Getters

    public bool IsRaw()
    {
        return !(IsCooked() || IsBurnt());
    }

    public bool IsCooked()
    {
        return cooked == true;
    }

    public bool IsBurnt()
    {
        return burnt == true;
    }

    // End Getters


    // Start is called before the first frame update
    void Start()
    {
        timeCooked = 0;
        cooked.SetColor("_albedoCook", Color.red);
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("Pan"))
        {
            if (IsCooked() == false)
            {
                if (timeCooked < 2)
                {
                    timeCooked += Time.deltaTime;
                    Debug.Log("Cooking Time: " + timeCooked + " seconds");
                }
                else
                {
                    cooked.SetColor("_albedoCook", Color.black);
                    //gameObject.SetActive(false);
                    //isCooked = true;
                    Debug.Log("Object is cooked");
                }
            }
        }
    }

    /**
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision Detected");
        if (other.gameObject.CompareTag("Pan"))
        {
            if (isCooked == false)
            {
                rend.material = cooked;
                //gameObject.SetActive(false);
                isCooked = true;
                Debug.Log("Object is cooked");
            }
        }
    }
    **/
}