using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelManagement;
using VRTK;

public class MinigameTeleporter : MonoBehaviour
{

    [Header("Pause Management")]
    public PauseManagerv2 pauseManager;

    [Header("Me")]
    public VRTK_InteractableObject interactable;

    void Start()
    {

    }

    void Update()
    {
        if (interactable.IsTouched())
        {
            pauseManager.SetLocationToBowling();
            pauseManager.SetPause();
        }
    }

}


