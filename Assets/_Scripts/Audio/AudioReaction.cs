using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioReaction : MonoBehaviour
{
    float cd = 0;
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
            if (sounds[i].isPlaying && cd == 0)
            {
                Debug.Log("AudioReaction.cs: playing");
                cd += Time.deltaTime;
                anim.SetTrigger("EarTrig");
            }

            else {
                anim.SetBool("EarGrowing", false);
            }
        }

        if (cd >= 5)
        {
            cd = 0;
        }
    }
}
