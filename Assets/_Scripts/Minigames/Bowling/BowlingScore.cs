using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BowlingScore : MonoBehaviour
{
    public TextMeshProUGUI strikeText;
    public TextMeshProUGUI spareText;
    public TextMeshProUGUI loseText;
    private int temp = 0;


    public BowlingRespawn bowlingRespawn;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pin"))
        {
            temp++;
            //Destroy(other);
            Debug.Log(temp);
            if (temp >= 6)
            {
                CheckWin();
            }
        }
    }

    private void CheckWin()
    {
        if (bowlingRespawn.ballsSpawned == 1)
        {
            strikeText.enabled = true;
        }

        else if (bowlingRespawn.ballsSpawned == 2)
        {
            spareText.enabled = true;
        }
        else
        {
            loseText.enabled = true;
        }
    }
}
