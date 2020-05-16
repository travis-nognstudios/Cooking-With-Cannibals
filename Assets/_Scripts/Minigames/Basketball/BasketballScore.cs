using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BasketballScore : MonoBehaviour
{
    //public TextMeshProUGUI scoreText;
    private float score;
    private AudioSource scoreSound;
    public ParticleSystem scoreParticle;
    void Start()
    {
        score = 0;
        scoreSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Respawn"))
        {
            other.tag = "Untagged";
            score++;
            //scoreText.text = score.ToString();
            //scoreSound.Play();
            scoreParticle.Play();
            
        }
    }
}
