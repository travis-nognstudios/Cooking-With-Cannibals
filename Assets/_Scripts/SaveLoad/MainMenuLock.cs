using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLock : MonoBehaviour
{
    public GameObject LevelOneLogo;
    public GameObject LevelTwoLogo;
    public GameObject LevelThreeLogo;
    // Start is called before the first frame update
    void Start()
    {
 
        if (SaveState.levelOneCompleted == false)
        {
            LevelTwoLogo.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            LevelTwoLogo.GetComponent<SpriteRenderer>().enabled = true;
        }

        if (SaveState.levelTwoCompleted == false)
        {
            LevelThreeLogo.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            LevelThreeLogo.GetComponent<SpriteRenderer>().enabled = true;
        }
    }


}
