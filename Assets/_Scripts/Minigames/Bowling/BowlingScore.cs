using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using LevelManagement;

public class BowlingScore : MonoBehaviour
{
    public GameObject strikeText;
    public GameObject spareText;
    public GameObject loseText;
    public PauseManagerv2 pauseManager;
    private int score = 0;


    public BowlingRespawn bowlingRespawn;

    private void Start()
    {
        pauseManager = GameObject.Find("PauseManagerv2").GetComponent<PauseManagerv2>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pin"))
        {

            score++;
            //Destroy(other);
            Debug.Log(score);
            if (score >= 6)
            {
                CheckWin();
            }
        }
    }

    private void CheckWin()
    {
        if (bowlingRespawn.ballsSpawned == 0)
        {
            strikeText.SetActive(true);
            pauseManager.SetUnpause();
        }

        else if (bowlingRespawn.ballsSpawned == 1)
        {
            spareText.SetActive(true);
            Debug.Log("Spare");
            pauseManager.SetUnpause();
        }
        else
        {
            loseText.SetActive(true);
            Debug.Log("Lose");
            pauseManager.SetUnpause();
        }
    }
}
