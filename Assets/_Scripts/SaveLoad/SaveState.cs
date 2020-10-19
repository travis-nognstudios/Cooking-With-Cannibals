using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Serving;
public class SaveState : MonoBehaviour
{
    private LevelOneSaveData levelOneSave;

    private LevelTwoSaveData levelTwoSave;
    
    private LevelThreeSaveData levelThreeSave;

    public static bool goldKnifeUnlocked;

    //[HideInInspector]
    public bool debug;

    //Debug Variables
    [HideInInspector]
    public bool setLevelOneCompleted;
    [HideInInspector]
    public float setLevelOneTime;
    [HideInInspector]
    public RatingCardSpawner.Rating setLevelOneHG;
    [HideInInspector]
    public int setLevelOneHS;

    [HideInInspector]
    public bool setLevelTwoCompleted;
    [HideInInspector]
    public float setLevelTwoTime;
    [HideInInspector]
    public RatingCardSpawner.Rating setLevelTwoHG;
    [HideInInspector]
    public int setLevelTwoHS;

    [HideInInspector]
    public bool setLevelThreeCompleted;
    [HideInInspector]
    public float setLevelThreeTime;
    [HideInInspector]
    public RatingCardSpawner.Rating setLevelThreeHG;
    [HideInInspector]
    public int setLevelThreeHS;




    private void Start()
    {
        levelOneSave = GetComponent<LevelOneSaveData>();
        levelTwoSave = GetComponent<LevelTwoSaveData>();
        levelThreeSave = GetComponent<LevelThreeSaveData>();

        if (!debug)
        {
            if(levelOneSave != null)
                levelOneSave.Load();
            if(levelTwoSave != null)
                levelTwoSave.Load();
            if(levelThreeSave != null)
                levelThreeSave.Load();
        }
        else
        {
            SetDebug();
        }

        ApplyGoldenKnifeUnlockRule();
        
    }

    public void SetDebug()
    {
        LevelOneSaveData.levelOneCompleted = setLevelOneCompleted;
        LevelOneSaveData.levelOneHG = setLevelOneHG;
        LevelOneSaveData.levelOneHS = setLevelOneHS;
        LevelOneSaveData.levelOneTime = setLevelOneTime;
        LevelTwoSaveData.levelTwoCompleted = setLevelTwoCompleted;
        LevelTwoSaveData.levelTwoHG = setLevelTwoHG;
        LevelTwoSaveData.levelTwoHS = setLevelTwoHS;
        LevelTwoSaveData.levelTwoTime = setLevelTwoTime;
        LevelThreeSaveData.levelThreeCompleted = setLevelThreeCompleted;
        LevelThreeSaveData.levelThreeHG = setLevelThreeHG;
        LevelThreeSaveData.levelThreeHS = setLevelThreeHS;
        LevelThreeSaveData.levelThreeTime = setLevelThreeTime;

    }

    //Called at end of service
    public void Save()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
            levelOneSave.Save();
        else if (SceneManager.GetActiveScene().buildIndex == 2)
            levelTwoSave.Save();
        else if (SceneManager.GetActiveScene().buildIndex == 3)
            levelThreeSave.Save();
    }
    public void ApplyGoldenKnifeUnlockRule()
    {
        if (LevelOneSaveData.levelOneHG == RatingCardSpawner.Rating.A &&
            LevelTwoSaveData.levelTwoHG == RatingCardSpawner.Rating.A &&
            LevelThreeSaveData.levelThreeHG == RatingCardSpawner.Rating.A)
        {
            goldKnifeUnlocked = true;
        }
        else
            goldKnifeUnlocked = false;
    }
}
