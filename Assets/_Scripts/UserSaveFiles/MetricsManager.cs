#region Script Description
/*
 * AUTHOR: Travis Hove
 * LAST UPDATED: 11/07/2020
 * TITLE: MetricsManager.cs
 * VERSION: 1.0
 * DESCRIPTION:
 * This script handles a Json file system that keeps track of a 
 * structure of serialized variables. This structure defines level
 * completion, time, score (tips), and grading system. 
 */
#endregion

using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class MetricsManager : MonoBehaviour
{
    public static MetricsManager instance = null;
    public List<LevelMetrics> userMetrics;
    public LevelMetrics metric;

    #region Initialize Save Level File
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadMetrics();
        }
        else if (instance != this)
            Destroy(gameObject);
    }

    public void InitLevelData()
    {
        LevelMetrics metric = new LevelMetrics(0.0f, 0, "incomplete", 0.0f, 0, "incomplete", 0.0f, 0, "incomplete");
    }
    #endregion

    #region Save Functions - Level One
    public void SaveLevelOneTime(float lvlone_tm)
    {
        if (instance.gameObject.activeInHierarchy)
        {
            userMetrics[0] = new LevelMetrics(lvlone_tm, userMetrics[0].lvlone_tp, userMetrics[0].lvlone_gr, userMetrics[0].lvltwo_tm, userMetrics[0].lvltwo_tp, userMetrics[0].lvltwo_gr, userMetrics[0].lvlthree_tm, userMetrics[0].lvlthree_tp, userMetrics[0].lvlthree_gr);
            SaveMetrics();
        }
    }

    public void SaveLevelOneTips(int lvlone_tp)
    {
        if (instance.gameObject.activeInHierarchy)
        {
            userMetrics[0] = new LevelMetrics(userMetrics[0].lvlone_tm, lvlone_tp, userMetrics[0].lvlone_gr, userMetrics[0].lvltwo_tm, userMetrics[0].lvltwo_tp, userMetrics[0].lvltwo_gr, userMetrics[0].lvlthree_tm, userMetrics[0].lvlthree_tp, userMetrics[0].lvlthree_gr);
            SaveMetrics();
        }
    }

    public void SaveLevelOneGrade(string lvlone_gr)
    {
        if (instance.gameObject.activeInHierarchy)
        {
            userMetrics[0] = new LevelMetrics(userMetrics[0].lvlone_tm, userMetrics[0].lvlone_tp, lvlone_gr, userMetrics[0].lvltwo_tm, userMetrics[0].lvltwo_tp, userMetrics[0].lvltwo_gr, userMetrics[0].lvlthree_tm, userMetrics[0].lvlthree_tp, userMetrics[0].lvlthree_gr);
            SaveMetrics();
        }
    }
    #endregion

    #region Save Functions - Level Two
    public void SaveLevelTwoTime(float lvltwo_tm)
    {
        if (instance.gameObject.activeInHierarchy)
        {
            userMetrics[0] = new LevelMetrics(userMetrics[0].lvlone_tm, userMetrics[0].lvlone_tp, userMetrics[0].lvlone_gr, lvltwo_tm, userMetrics[0].lvltwo_tp, userMetrics[0].lvltwo_gr, userMetrics[0].lvlthree_tm, userMetrics[0].lvlthree_tp, userMetrics[0].lvlthree_gr);
            SaveMetrics();
        }
    }

    public void SaveLevelTwoTips(int lvltwo_tp)
    {
        if (instance.gameObject.activeInHierarchy)
        {
            userMetrics[0] = new LevelMetrics(userMetrics[0].lvlone_tm, userMetrics[0].lvlone_tp, userMetrics[0].lvlone_gr, userMetrics[0].lvltwo_tm, lvltwo_tp, userMetrics[0].lvltwo_gr, userMetrics[0].lvlthree_tm, userMetrics[0].lvlthree_tp, userMetrics[0].lvlthree_gr);
            SaveMetrics();
        }
    }

    public void SaveLevelTwoGrade(string lvltwo_gr)
    {
        if (instance.gameObject.activeInHierarchy)
        {
            userMetrics[0] = new LevelMetrics(userMetrics[0].lvlone_tm, userMetrics[0].lvlone_tp, userMetrics[0].lvlone_gr, userMetrics[0].lvltwo_tm, userMetrics[0].lvltwo_tp, lvltwo_gr, userMetrics[0].lvlthree_tm, userMetrics[0].lvlthree_tp, userMetrics[0].lvlthree_gr);
            SaveMetrics();
        }
    }
    #endregion

    #region Save Functions - Level Three
    public void SaveLevelThreeTime(float lvlthree_tm)
    {
        if (instance.gameObject.activeInHierarchy)
        {
            userMetrics[0] = new LevelMetrics(userMetrics[0].lvlone_tm, userMetrics[0].lvlone_tp, userMetrics[0].lvlone_gr, userMetrics[0].lvltwo_tm, userMetrics[0].lvltwo_tp, userMetrics[0].lvltwo_gr, lvlthree_tm, userMetrics[0].lvlthree_tp, userMetrics[0].lvlthree_gr);
            SaveMetrics();
        }
    }

    public void SaveLevelThreeTips(int lvlthree_tp)
    {
        if (instance.gameObject.activeInHierarchy)
        {
            userMetrics[0] = new LevelMetrics(userMetrics[0].lvlone_tm, userMetrics[0].lvlone_tp, userMetrics[0].lvlone_gr, userMetrics[0].lvltwo_tm, userMetrics[0].lvltwo_tp, userMetrics[0].lvltwo_gr, userMetrics[0].lvlthree_tm, lvlthree_tp, userMetrics[0].lvlthree_gr);
            SaveMetrics();
        }
    }

    public void SaveLevelThreeGrade(string lvlthree_gr)
    {
        if (instance.gameObject.activeInHierarchy)
        {
            userMetrics[0] = new LevelMetrics(userMetrics[0].lvlone_tm, userMetrics[0].lvlone_tp, userMetrics[0].lvlone_gr, userMetrics[0].lvltwo_tm, userMetrics[0].lvltwo_tp, userMetrics[0].lvltwo_gr, userMetrics[0].lvlthree_tm, userMetrics[0].lvlthree_tp, lvlthree_gr);
            SaveMetrics();
        }
    }
    #endregion

    #region Universal Functions
    public void SaveMetrics()
    {
        string json;
        string file = Application.persistentDataPath + "/LevelProgress.json";
        if (!File.Exists(file))
        {
            LevelData data = new LevelData
            {
                userMetrics = { }
            };
            //initialize a new file with empty data
            json = JsonUtility.ToJson(data);
        }
        else;
        {
            //save current game state
            json = JsonUtility.ToJson(instance);
        }

        LevelData save = JsonUtility.FromJson<LevelData>(json);
        File.WriteAllText(file, json);
    }

    public void LoadMetrics()
    {
        string filePath = Application.persistentDataPath + "/LevelProgress.json";
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            LevelData gameData = JsonUtility.FromJson<LevelData>(dataAsJson);
            userMetrics = gameData.userMetrics;
        }
        else
        {
            SaveMetrics();
        }
    }
    #endregion

    #region Serialized Level Data
    [Serializable]
    class LevelData
    {
        public List<LevelMetrics> userMetrics = new List<LevelMetrics>();
    }

    [System.Serializable]
    public struct LevelMetrics
    {
        #region Level One Metrics
        public float lvlone_tm;
        public int lvlone_tp;
        public string lvlone_gr;
        #endregion

        #region Level Two Metrics
        public float lvltwo_tm;
        public int lvltwo_tp;
        public string lvltwo_gr;
        #endregion

        #region Level Three Metrics
        public float lvlthree_tm;
        public int lvlthree_tp;
        public string lvlthree_gr;
        #endregion


        public LevelMetrics(float lvlone_tm, int lvlone_tp, string lvlone_gr, float lvltwo_tm, int lvltwo_tp, string lvltwo_gr, float lvlthree_tm, int lvlthree_tp, string lvlthree_gr)
        {
            #region Level One Metrics Returns
            this.lvlone_tm = lvlone_tm;
            this.lvlone_tp = lvlone_tp;
            this.lvlone_gr = lvlone_gr;
            #endregion

            #region Level Two Metrics Return
            this.lvltwo_tm = lvltwo_tm;
            this.lvltwo_tp = lvltwo_tp;
            this.lvltwo_gr = lvltwo_gr;
            #endregion

            #region Level Three Metrics Return
            this.lvlthree_tm = lvlthree_tm;
            this.lvlthree_tp = lvlthree_tp;
            this.lvlthree_gr = lvlthree_gr;
            #endregion
        }
        #endregion
    }
}
