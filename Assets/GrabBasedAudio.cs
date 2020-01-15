using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class GrabBasedAudio : MonoBehaviour
{

    public AudioSource soundSource;
    public AudioClip dropSound;
    public AudioClip interactSound;
    public AudioClip cookSound;
    public float dropSpeed = 1f;
    private bool isGrabbed = false;

    // Start is called before the first frame update
    void Start()
    {
        soundSource = this.GetComponent<AudioSource>();
        soundSource.clip = dropSound;
        //Debug.Log("Start works");
    }

    public void SetInteractSound()
    {
        soundSource.clip = interactSound;
    }

    public void SetDropSound()
    {
        soundSource.clip = dropSound;
    }

    public void SetCookSound()
    {
        soundSource.clip = cookSound;
    }
}
