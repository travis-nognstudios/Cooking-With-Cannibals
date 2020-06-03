using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Serving;
using UnityEngine.SceneManagement;
public class SaveState : MonoBehaviour
{
    public OrderSpawnerv5 orderSpawner;
    public TipJar tipJar;
    public static bool tutorialCompleted = false;
    public static bool levelOneCompleted = false;
    public static bool levelTwoCompleted = false;
    public static bool levelThreeCompleted = false;
    public static int levelOneHS;
    public static int levelTwoHS;
    public static int levelThreeHS;
    public static int levelOneEHS;
    public static int levelTwoEHS;
    public static int levelThreeEHS;


    private void Start()
    {
        
        if (SaveLoad.SaveExists("LevelOneProgress"))
        {
            levelOneCompleted = SaveLoad.Load<bool>("LevelOneProgress");
        }
        if (SaveLoad.SaveExists("LevelTwoProgress"))
        {
            levelTwoCompleted = SaveLoad.Load<bool>("LevelTwoProgress");

        }
        if (SaveLoad.SaveExists("LevelThreeProgress"))
        {
            levelThreeCompleted = SaveLoad.Load<bool>("LevelThreeProgress");
        }
        if (SaveLoad.SaveExists("LevelOneHS"))
        {
            levelOneHS = SaveLoad.Load<int>("LevelOneHS");
        }
        if (SaveLoad.SaveExists("LevelTwoHS"))
        {
            levelTwoHS = SaveLoad.Load<int>("LevelTwoHS");
        }
        if (SaveLoad.SaveExists("LevelTwoHS"))
        {
            levelTwoHS = SaveLoad.Load<int>("LevelTwoHS");
        }

}

    public void Save()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            levelOneCompleted = true;
            SaveLoad.Save(levelOneCompleted, "LevelOneProgress");

            if (SaveLoad.SaveExists("LevelOneHS"))
            {
                if (tipJar.GetAmountInJar() > SaveLoad.Load<int>("LevelOneHS"))
                {
                    SaveLoad.Save(tipJar.GetAmountInJar(), "LevelOneHS");
                }

            }
            else
            {
                SaveLoad.Save(tipJar.GetAmountInJar(), "LevelOneHS");
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            levelTwoCompleted = true;
            SaveLoad.Save(levelTwoCompleted, "LevelTwoProgress");

            if (SaveLoad.SaveExists("LevelTwoHS"))
            {
                if (tipJar.GetAmountInJar() > SaveLoad.Load<int>("LevelTwoHS"))
                {
                    SaveLoad.Save(tipJar.GetAmountInJar(), "LevelTwoHS");
                }

            }
            else
            {
                SaveLoad.Save(tipJar.GetAmountInJar(), "LevelTwoHS");
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            levelThreeCompleted = true;
            SaveLoad.Save(levelThreeCompleted, "LevelThreeProgress");

            if (SaveLoad.SaveExists("LevelTwoHS"))
            {
                if (tipJar.GetAmountInJar() > SaveLoad.Load<int>("LevelTwoHS"))
                {
                    SaveLoad.Save(tipJar.GetAmountInJar(), "LevelTwoHS");
                }

            }
            else
            {
                SaveLoad.Save(tipJar.GetAmountInJar(), "LevelTwoHS");
            }
        }
    }
}
