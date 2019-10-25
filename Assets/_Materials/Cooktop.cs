using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooktop : MonoBehaviour
{
    #region Variables

    public enum Cooktype { Grill, Boil, Deepfry };

    private bool IsHot;

    [Header("Cooktop Settings")]
    public Cooktype cooktype;

    #endregion Variables

    // Start is called before the first frame update
    void Start()
    {
        MakeCold();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Heatsource"))  
        {
            MakeHot();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Heatsource"))
        {
            MakeCold();
        }
    }

    void MakeHot()
    {
        IsHot = true;
        //Debug.Log("Now hot");
    }

    void MakeCold()
    {
        IsHot = false;
        //Debug.Log("Now cold");
    }
}