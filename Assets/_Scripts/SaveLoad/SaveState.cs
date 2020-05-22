using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Serving;
using UnityEngine.SceneManagement;
public class SaveState : MonoBehaviour
{

    public OrderSpawnerv5 orderSpawner;
    public static bool levelOneCompleted = false;
    public static bool levelTwoCompleted = false;
    public static bool levelThreeCompleted = false;

    private void Start()
    {
        
        if (SaveLoad.SaveExists("Level One Progress"))
        {
            levelOneCompleted = SaveLoad.Load<bool>("Level One Progress");
        }
        if (SaveLoad.SaveExists("Level Two Progress"))
        {
            levelTwoCompleted = SaveLoad.Load<bool>("Level Two Progress");

        }
        if (SaveLoad.SaveExists("Level Three Progress"))
        {
            levelThreeCompleted = SaveLoad.Load<bool>("Level Three Progress");
            Debug.Log(levelOneCompleted);
            Debug.Log(levelTwoCompleted);
            Debug.Log(levelThreeCompleted);
        }
    }

    private void Update()
    {
        if (orderSpawner.IsServiceOver())
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                levelOneCompleted = true;
                Save();
            }
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                levelTwoCompleted = true;
                Save();
            }
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                levelThreeCompleted = true;
                Save();
            }
        }
    }

    public void Save()
    {
        SaveLoad.Save(levelOneCompleted, "Level One Progress");
        SaveLoad.Save(levelTwoCompleted, "Level Two Progress");
        SaveLoad.Save(levelThreeCompleted, "Level Three Progress");
    }
}
