using UnityEngine;
using System.Collections;
using System;
using Cooking;

public class LevelPlate : MonoBehaviour
{

    Cookable[] otherObjects;

    #region Variables

    [Header("Recipes")]
    public Cooking.Cookable.Ingredient[] recipes; //ToDo: Make actual recipes, not ingredients

    private bool[] contains;
    private bool done;

    #endregion Variables


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void CheckDone()
    {
        if (System.Array.TrueForAll<bool>(contains, IsTrue))
        {
            done = true;
        }
    }

    private bool IsTrue(bool item)
    {
        return item == true;
    }

    void OnTriggerEnter(Collider other)
    {
        Cookable otherScript = other.GetComponent<Cookable>();

        if (other.gameObject.CompareTag("Cookable"))
        {
            Cooking.Cookable.Ingredient ingrType = otherScript.ingredientType;
            if (ingrType == Cookable.Ingredient.Hand)
            {
                if (otherScript.IsCooked())
                {
                    contains[GetArrayIndex(ingrType)] = true;
                }
            }
            else
            {
                contains[GetArrayIndex(ingrType)] = true;
            }

            CheckDone();
        }
    }

    private int GetArrayIndex(Cooking.Cookable.Ingredient ingr)
    {
        return System.Array.IndexOf(recipes, ingr);
    }
}
