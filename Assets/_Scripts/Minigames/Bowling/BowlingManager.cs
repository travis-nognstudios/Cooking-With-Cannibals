using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using LevelManagement;

public class BowlingManager : MonoBehaviour
{
    public GameObject pinSpawn;
    public GameObject pins;
    private int score = 0;

    public float despawnTime;


    public BowlingRespawn bowlingRespawn;

    private void Start()
    {
        //pauseManager = GameObject.Find("PauseManagerv2").GetComponent<PauseManagerv2>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pin"))
        {
            other.tag = "Untagged";
            score++;

            StartCoroutine(DestroyAfter(other, despawnTime));
            if (score >= 10)
            {
                StartCoroutine(ResetGame());
                //CheckWin();
            }
        }
    }
    /*
    private void CheckWin()
    {
        if (bowlingRespawn.ballsSpawned == 0)
        {
            strikeText.SetActive(true);
            StartCoroutine("ResetGame");
        }

        else if (bowlingRespawn.ballsSpawned == 1)
        {
            spareText.SetActive(true);
            StartCoroutine("ResetGame");
        }
        else
        {
            loseText.SetActive(true);
            StartCoroutine("ResetGame");
        }
    }
    */
    public IEnumerator DestroyAfter(Collider other, float despawnTime)
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(other.gameObject.transform.parent.gameObject);
        
    }
    private IEnumerator ResetGame()
    {
        //pauseManager.SetUnpause();
        yield return new WaitForSeconds(3f);
        score = 0;
        Instantiate(pins, pinSpawn.transform);
        


    }
}
