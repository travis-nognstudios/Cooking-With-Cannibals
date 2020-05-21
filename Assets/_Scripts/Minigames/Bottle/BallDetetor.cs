using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDetector : MonoBehaviour
{

    public int ballsUsed = 0;
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Respawn"))
        {
            other.tag = "Untagged";
            StartCoroutine(BallCount());
        }
    }

    private IEnumerator BallCount()
    {
        yield return new WaitForSeconds(3f);
        ballsUsed++;
    }
}
