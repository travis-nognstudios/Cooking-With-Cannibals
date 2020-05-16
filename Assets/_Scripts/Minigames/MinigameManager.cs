using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameManager : MonoBehaviour
{
    [HideInInspector]
    public bool playing;
    public GameObject minigame;
    public float minigameTimer;
    private float timer;
    public Slider loadingSlider;
    public GameObject loadingBar;
    

    [Header("Level Two")]
    public GameObject customers2;

    [Header("Level Three")]
    public GameObject customers3;
    public GameObject tables;




    private void Start()
    {
        timer = 0;
        playing = false;
        minigame.SetActive(false);
        loadingBar.SetActive(false);
    }
    private void Update()
    {
        if (playing)
        {
            timer += Time.deltaTime;
            loadingSlider.value = timer;
        }
    }

    public void Play()
    {
        playing = true;
        loadingBar.SetActive(true);
        minigame.SetActive(true);

        if (customers2 != null)
         customers2.SetActive(false);
        

        if(tables != null)
            tables.SetActive(false);
        if(customers3 != null)
            customers3.SetActive(false);


    }
    public bool Over()
    {
        if (timer >= minigameTimer)
        {
            playing = false;
            return true;
        }
        else
            return false;
    }
}
