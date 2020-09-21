using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Serving;
using UnityEngine.SceneManagement;
public class LevelTwoSaveData : MonoBehaviour
{
    private OrderSpawnerv5 orderSpawner;
    private TipJar tipJar;
    private RatingCardSpawner ratingCardSpawner;

    public static bool levelTwoCompleted = false;
    public static float levelTwoTime; //Best time
    public static RatingCardSpawner.Rating levelTwoHG = RatingCardSpawner.Rating.F; //Highest letter grade
    public static int levelTwoHS; //Highest number score


    private void Start()
    {
        orderSpawner = FindObjectOfType<OrderSpawnerv5>();
        tipJar = FindObjectOfType<TipJar>();
        ratingCardSpawner = FindObjectOfType<RatingCardSpawner>();
    }

    public void Load()
    {
        if (SaveLoad.SaveExists("LevelTwoProgress"))
        {
            levelTwoCompleted = SaveLoad.Load<bool>("LevelTwoProgress");
        }
        if (SaveLoad.SaveExists("LevelTwoHS"))
        {
            levelTwoHS = SaveLoad.Load<int>("LevelTwoHS");
        }
        if (SaveLoad.SaveExists("LevelTwoTime"))
        {
            levelTwoTime = SaveLoad.Load<float>("LevelTwoTime");
        }
        if (SaveLoad.SaveExists("LevelTwoHG"))
        {
            levelTwoHG = SaveLoad.Load<RatingCardSpawner.Rating>("LevelTwoHG");
        }
    }
    public void Save()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
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

            if (SaveLoad.SaveExists("LevelTwoHG"))
            {
                if (ratingCardSpawner.GetRating() < SaveLoad.Load<RatingCardSpawner.Rating>("LevelTwoHG"))
                {
                    SaveLoad.Save(ratingCardSpawner.GetRating(), "LevelTwoHG");
                }
            }
            else
            {
                SaveLoad.Save(ratingCardSpawner.GetRating(), "LevelTwoHG");
            }
        }

    }
}
