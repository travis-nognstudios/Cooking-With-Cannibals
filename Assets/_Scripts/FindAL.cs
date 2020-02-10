using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindAL : MonoBehaviour
{
    public AudioListener AL;

    private void Start()
    {
        AL = FindObjectOfType<AudioListener>();
        Debug.Log("Audio" + AL);
    }
}


