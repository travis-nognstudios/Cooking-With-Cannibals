using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BasketballScore : MonoBehaviour
{
    public TMP_Text text;
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
            score++;
            text.text = score.ToString();
            scoreSound.Play();
            scoreParticle.Play();
            other.tag = "Untagged";
        }
    }
}
