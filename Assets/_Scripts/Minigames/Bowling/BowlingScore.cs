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
    //public PauseManagerv2 pauseManager;
    private int score = 0;


    public BowlingRespawn bowlingRespawn;

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

        }

        else if (bowlingRespawn.ballsSpawned == 1)
        {
            spareText.SetActive(true);
            Debug.Log("Spare");

        }
        else
        {
            loseText.SetActive(true);
            Debug.Log("Lose");
        }
    }
}
