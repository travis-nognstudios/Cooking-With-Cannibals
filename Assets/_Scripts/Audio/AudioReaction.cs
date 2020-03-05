using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioReaction : MonoBehaviour
{
    
    public AudioSource[] sounds;
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        //sounds = FindObjectsOfType<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].isPlaying)
            {
                //Debug.Log("AudioReaction.cs: playing");

                anim.SetTrigger("EarTrig");
            }
        }
    }
}
