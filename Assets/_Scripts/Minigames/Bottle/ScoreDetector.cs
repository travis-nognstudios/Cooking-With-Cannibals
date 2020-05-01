using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDetector : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int temp = 0;
    private int score = 0;
    public BottleSpawner spawner;
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
            temp++;
            Destroy(other);
            Debug.Log(temp);
            if (temp >= 6)
            {
                temp = 0;
                score++;
                spawner.Spawn();
            }
        }
    }
}
