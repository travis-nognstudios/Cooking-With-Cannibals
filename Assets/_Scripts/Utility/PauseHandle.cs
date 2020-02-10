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
        if (gameObject.transform.rotation.x < -80)
        {
            LevelManager.Instance.LoadScene(0);
        }
    }
}
