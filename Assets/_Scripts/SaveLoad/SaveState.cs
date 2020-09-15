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


    private void Start()
    {
        levelOneSave = GetComponent<LevelOneSaveData>();
        levelTwoSave = GetComponent<LevelTwoSaveData>();
        levelThreeSave = GetComponent<LevelThreeSaveData>();

        levelOneSave.Load();
        levelTwoSave.Load();
        levelThreeSave.Load();

        ApplyGoldenKnifeUnlockRule();

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
