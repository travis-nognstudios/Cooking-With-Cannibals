using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRespawn : MonoBehaviour
{
    #region Variables

    [SerializeField]

   
    //private GameObject Original;

    private Vector3 ObjPos;
    private Quaternion ObjRot;
    
    Vector3 Offset = new Vector3(0, 0.1f, 0);
    


    #endregion

    void Start()
    {
       
       
            //ObjPos = Original.transform.position;
            //ObjRot = Original.transform.rotation;

            ObjPos = this.transform.position;
            ObjRot = this.transform.rotation;
        //Debug.Log("At Start Position: " + ObjPos + "   Rotation: " + ObjRot);
       
    }

    private void OnTriggerEnter(Collider col)
    {
    
        if (col.gameObject.tag == "destroy")
        {
            this.transform.position = ObjPos;
            this.transform.rotation = ObjRot;


            //Original.transform.position = ObjPos;
            //Original.transform.rotation = ObjRot;

        }
    }
}

