using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Serving;
using UnityEngine.SceneManagement;
public class SaveState : MonoBehaviour
{
    public OrderSpawnerv5 orderSpawner;
    public TipJar tipJar;

    //public TipJar easterEggTips;

    //Level Progress
    public static bool tutorialCompleted = false;
    public static bool levelOneCompleted = false;
    public static bool levelTwoCompleted = false;
    public static bool levelThreeCompleted = false;

    //High Scores
    public static int levelOneHS;
    public static int levelTwoHS;
    public static int levelThreeHS;

    //Best Times
    public static float levelOneTime;
    public static float levelTwoTime;
    public static float levelThreeTime;

    //EasterEgg High Scores
    public static int levelOneEHS;
    public static int levelTwoEHS;
    public static int levelThreeEHS;


    public static bool goldKnifeUnlocked;


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
        if (SaveLoad.SaveExists("GoldKnifeUnlocked"))
        {
            goldKnifeUnlocked = SaveLoad.Load<bool>("GoldKnifeUnlocked");
        }
        if (SaveLoad.SaveExists("LevelOneHS"))
        {
            levelOneHS = SaveLoad.Load<int>("LevelOneHS");
        }
        if (SaveLoad.SaveExists("LevelTwoHS"))
        {
            levelTwoHS = SaveLoad.Load<int>("LevelTwoHS");
        }
        if (SaveLoad.SaveExists("LevelThreeHS"))
        {
            levelThreeHS = SaveLoad.Load<int>("LevelThreeHS");
        }if (SaveLoad.SaveExists("LevelOneEHS"))
        {
            levelOneEHS = SaveLoad.Load<int>("LevelOneEHS");
        }
        if (SaveLoad.SaveExists("LevelTwoEHS"))
        {
            levelTwoEHS = SaveLoad.Load<int>("LevelTwoEHS");
        }
        if (SaveLoad.SaveExists("LevelThreeEHS"))
        {
            levelThreeEHS = SaveLoad.Load<int>("LevelThreeEHS");
        }
        if (SaveLoad.SaveExists("LevelOneTime"))
        {
            levelOneTime = SaveLoad.Load<float>("LevelOneTime");
        }
        if (SaveLoad.SaveExists("LevelTwoTime"))
        {
            levelTwoTime = SaveLoad.Load<float>("LevelTwoTime");
        }
        if (SaveLoad.SaveExists("LevelThreeTime"))
        {
            levelThreeTime = SaveLoad.Load<float>("LevelThreeTime");
        }

        ApplyGoldenKnifeUnlockRule();

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

            if (SaveLoad.SaveExists("LevelOneTime"))
            {
                if (orderSpawner.timeSinceServiceStarted < SaveLoad.Load<float>("LevelOneTime"))
                {
                    SaveLoad.Save(orderSpawner.timeSinceServiceStarted, "LevelOneTime");
                }
            }
            else
            {
                SaveLoad.Save(orderSpawner.timeSinceServiceStarted, "LevelOneTime");
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

            if (SaveLoad.SaveExists("LevelTwoTime"))
            {
                if (orderSpawner.timeSinceServiceStarted < SaveLoad.Load<float>("LevelTwoTime"))
                {
                    SaveLoad.Save(orderSpawner.timeSinceServiceStarted, "LevelTwoTime");
                }
            }
            else
            {
                SaveLoad.Save(orderSpawner.timeSinceServiceStarted, "LevelTwoTime");
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            levelThreeCompleted = true;
            SaveLoad.Save(levelThreeCompleted, "LevelThreeProgress");

            if (SaveLoad.SaveExists("LevelThreeHS"))
            {
                if (tipJar.GetAmountInJar() > SaveLoad.Load<int>("LevelThreeHS"))
                {
                    SaveLoad.Save(tipJar.GetAmountInJar(), "LevelThreeHS");
                }

            }
            else
            {
                SaveLoad.Save(tipJar.GetAmountInJar(), "LevelThreeHS");
            }

            if (SaveLoad.SaveExists("LevelThreeTime"))
            {
                if (orderSpawner.timeSinceServiceStarted < SaveLoad.Load<float>("LevelThreeTime"))
                {
                    SaveLoad.Save(orderSpawner.timeSinceServiceStarted, "LevelThreeTime");
                }
            }
            else
            {
                SaveLoad.Save(orderSpawner.timeSinceServiceStarted, "LevelThreeTime");
            }
        }
    }

    public void ApplyGoldenKnifeUnlockRule()
    {
        // Implementation:
        // Check if all 3 levels have highest grade achieved = A
        // if yes, then set the knife unlocked state to true
    }


    /*
    public void SaveMinigame()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (SaveLoad.SaveExists("LevelOneEHS"))
            {
                if (easterEggTips.GetAmountInJar() > SaveLoad.Load<int>("LevelOneEHS"))
                {
                    SaveLoad.Save(easterEggTips.GetAmountInJar(), "LevelOneEHS");
                }

            }
            else
            {
                SaveLoad.Save(easterEggTips.GetAmountInJar(), "LevelOneEHS");
            }
        }
    }
    */
}
