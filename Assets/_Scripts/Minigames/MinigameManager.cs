using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameManager : MonoBehaviour
{
    public bool playing;
    public float minigameTimer;
    private float timer;
    public Slider loadingSlider;
    public GameObject loadingBar;
    private void Start()
    {
        timer = 0;
        playing = false;
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
