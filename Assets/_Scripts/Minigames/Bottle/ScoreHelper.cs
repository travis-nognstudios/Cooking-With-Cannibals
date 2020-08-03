using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHelper : MonoBehaviour
{
    public ScoreDetector scoreDetector;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bottle"))
        {
            other.tag = "Untagged";
            scoreDetector.temp++;

            StartCoroutine(scoreDetector.DestroyAfter(other, scoreDetector.despawnTime));

            if (scoreDetector.temp >= 6)
            {
                scoreDetector.temp = 0;
                scoreDetector.score++;
                StartCoroutine(scoreDetector.RespawnBottles(scoreDetector.respawnTime));
            }
        }
    }
}
