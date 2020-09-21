using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelManagement;

public class MainMenuSave : MonoBehaviour
{
    public GameObject LevelOneLogo;
    public GameObject LevelTwoLogo;
    public GameObject LevelThreeLogo;
    private LevelOneSaveData levelOneSave;
    private LevelTwoSaveData levelTwoSave;
    private LevelThreeSaveData levelThreeSave;
    public SaveState saveState;
    // Start is called before the first frame update
    void Start()
    {
        levelOneSave = FindObjectOfType<LevelOneSaveData>();
        levelTwoSave = FindObjectOfType<LevelTwoSaveData>();
        levelThreeSave = FindObjectOfType<LevelThreeSaveData>();

        if (!saveState.debug)
        {
            levelOneSave.Load();
            levelTwoSave.Load();
            levelThreeSave.Load();
        }
        else
        {
            saveState.SetDebug();
        }
        if (LevelOneSaveData.levelOneCompleted == false || LevelOneSaveData.levelOneHG == Serving.RatingCardSpawner.Rating.F)
        {
            LevelTwoLogo.SetActive(false);
        }

        if (LevelTwoSaveData.levelTwoCompleted == false || LevelTwoSaveData.levelTwoHG == Serving.RatingCardSpawner.Rating.F)
        {
            LevelThreeLogo.SetActive(false);
        }
    }


}
