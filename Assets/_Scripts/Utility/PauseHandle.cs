using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseHandle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(gameObject.transform.eulerAngles.x);
        if (gameObject.transform.localEulerAngles.x == 270f)
        {
            
            LevelManager.Instance.LoadScene(0);
        }
    }
}
