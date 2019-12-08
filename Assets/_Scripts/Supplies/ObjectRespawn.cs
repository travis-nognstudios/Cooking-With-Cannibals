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
        ObjPos = this.transform.position;
        ObjRot = this.transform.rotation;
       
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "destroy")
        {
            this.transform.position = ObjPos;
            this.transform.rotation = ObjRot;
        }
    }
}

