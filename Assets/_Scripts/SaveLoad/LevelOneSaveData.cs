using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Serving;
public class LevelOneSaveData : MonoBehaviour
{
    private OrderSpawnerv5 orderSpawner;
    private TipJar tipJar;
    private RatingCardSpawner ratingCardSpawner;


    public static bool levelOneCompleted = false;
    public static float levelOneTime; //Best time
    public static RatingCardSpawner.Rating levelOneHG = RatingCardSpawner.Rating.F; //Highest letter grade
    public static int levelOneHS; //Highest number score


    private void Start()
    {
        orderSpawner = FindObjectOfType<OrderSpawnerv5>();
        tipJar = FindObjectOfType<TipJar>();
        ratingCardSpawner = FindObjectOfType<RatingCardSpawner>();

        //Tests
        /*
        Load();
        */


    }

    public void Load()
    {
        if (SaveLoad.SaveExists("LevelOneProgress"))
        {
            levelOneCompleted = SaveLoad.Load<bool>("LevelOneProgress");
        }
        if (SaveLoad.SaveExists("LevelOneHS"))
        {
            levelOneHS = SaveLoad.Load<int>("LevelOneHS");
        }
        if (SaveLoad.SaveExists("LevelOneTime"))
        {
            levelOneTime = SaveLoad.Load<float>("LevelOneTime");
        }
        if (SaveLoad.SaveExists("LevelOneHG"))
        {
            levelOneHG = SaveLoad.Load<RatingCardSpawner.Rating>("LevelOneHG");
        }

    }

    public void Save()
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

            if (SaveLoad.SaveExists("LevelOneHG"))
            {
                if (ratingCardSpawner.GetRating() < SaveLoad.Load<RatingCardSpawner.Rating>("LevelOneHG"))
                {
                    SaveLoad.Save(ratingCardSpawner.GetRating(), "LevelOneHG");
                }
            }
            else
            {
                SaveLoad.Save(ratingCardSpawner.GetRating(), "LevelOneHG");
            }
    }

    
}
