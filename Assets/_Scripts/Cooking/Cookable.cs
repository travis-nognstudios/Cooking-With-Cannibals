using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cookable : MonoBehaviour
{
    #region Variables

    public enum Cooktype { Grill, Boil, Deepfry };

    [Header("Cooking Settings")]
    public Cooktype cooktype;
    public float timeToCook;
    public float timeToBurn;

    private float timeCooked;
    private bool cooked;
    private bool burnt;

    [Header("Textures")]
    public Material[] material;
    Renderer rend;

    #endregion Variables

    
    #region Properties

    /// <summary>
    /// Whether the object is raw
    /// </summary>
    /// <returns></returns>
    private bool IsRaw()
    {
        return !(IsCooked() || IsBurnt());
    }

    /// <summary>
    /// Whether the object is cooked
    /// </summary>
    /// <returns></returns>
    private bool IsCooked()
    {
        return cooked == true && burnt == false;
    }

    /// <summary>
    /// Whether the object is burnt
    /// </summary>
    /// <returns></returns>
    private bool IsBurnt()
    {
        return burnt == true;
    }
    
    #endregion Properties



    #region MonoBehavior
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider other)
    {
        //ToDo: Add a check that cooktop's cooktype matches my cooktype
        //Removing it for the demo
        if (other.gameObject.CompareTag("Cooktop"))
        {
            timeCooked += Time.deltaTime;

            if (!IsCooked() && timeCooked >= timeToCook && timeCooked < timeToBurn)
            {
                MakeCooked();
            }
            else if (!IsBurnt() && timeCooked >= timeToBurn)
            {
                MakeBurnt();
            }
        }
    }
    #endregion MonoBehavior

    #region Private Methods
    private void MakeCooked()
    {
        cooked = true;
        rend.sharedMaterial = material[1];
        Debug.Log("Cooked");
    }

    private void MakeBurnt()
    {
        burnt = true;
        rend.sharedMaterial = material[2];
        Debug.Log("Burnt");
    }
    #endregion Private Methods

}