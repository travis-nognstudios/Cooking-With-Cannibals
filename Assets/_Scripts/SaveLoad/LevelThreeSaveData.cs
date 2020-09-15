using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Serving;
using UnityEngine.SceneManagement;
public class LevelThreeSaveData : MonoBehaviour
{
    private OrderSpawnerv5 orderSpawner;
    private TipJar tipJar;
    private RatingCardSpawner ratingCardSpawner;

    public static bool levelThreeCompleted = false;
    public static float levelThreeTime; //Best time
    public static RatingCardSpawner.Rating levelThreeHG = RatingCardSpawner.Rating.F; //Highest letter grade
    public static int levelThreeHS; //Highest number score


    private void Start()
    {
        orderSpawner = FindObjectOfType<OrderSpawnerv5>();
        tipJar = FindObjectOfType<TipJar>();
        ratingCardSpawner = FindObjectOfType<RatingCardSpawner>();
    }

    public void Load()
    {
        if (SaveLoad.SaveExists("LevelThreeProgress"))
        {
            levelThreeCompleted = SaveLoad.Load<bool>("LevelThreeProgress");
        }
        if (SaveLoad.SaveExists("LevelThreeHS"))
        {
            levelThreeHS = SaveLoad.Load<int>("LevelThreeHS");
        }
        if (SaveLoad.SaveExists("LevelThreeTime"))
        {
            levelThreeTime = SaveLoad.Load<float>("LevelThreeTime");
        }
        if (SaveLoad.SaveExists("LevelThreeHG"))
        {
            levelThreeHG = SaveLoad.Load<RatingCardSpawner.Rating>("LevelThreeHG");
        }
    }

    public void Save()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3)
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

            if (SaveLoad.SaveExists("LevelThreeHG"))
            {
                if (ratingCardSpawner.GetRating() < SaveLoad.Load<RatingCardSpawner.Rating>("LevelThreeHG"))
                {
                    SaveLoad.Save(ratingCardSpawner.GetRating(), "LevelThreeHG");
                }
            }
            else
            {
                SaveLoad.Save(ratingCardSpawner.GetRating(), "LevelThreeHG");
            }
        }

    }
}
