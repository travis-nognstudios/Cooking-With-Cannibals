using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDetector : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    [HideInInspector]
    public int temp = 0;

    [HideInInspector]
    public int score = 0;
    public BottleSpawner spawner;

    public float despawnTime;
    public float respawnTime;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bottle"))
        {
            other.tag = "Untagged";
            temp++;

            StartCoroutine(DestroyAfter(other, despawnTime));

            if (temp >= 6)
            {
                temp = 0;
                score++;
                StartCoroutine(RespawnBottles(respawnTime));
            }
        }
    }

    public IEnumerator DestroyAfter(Collider other, float despawnTime)
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(other.gameObject.transform.parent.gameObject);

    }

    public IEnumerator RespawnBottles(float respawnTime)
    {
        //pauseManager.SetUnpause();
        yield return new WaitForSeconds(respawnTime);
        //score = 0;
        spawner.Spawn();

    }
}
