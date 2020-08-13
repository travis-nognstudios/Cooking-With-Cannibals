using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelManagement;

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
            LevelTwoLogo.SetActive(false);
        }
        else
        {
            LevelTwoLogo.SetActive(true);
        }

        if (SaveState.levelTwoCompleted == false)
        {
            LevelThreeLogo.SetActive(false);
        }
        else
        {
            LevelThreeLogo.SetActive(true);
        }
    }


}
