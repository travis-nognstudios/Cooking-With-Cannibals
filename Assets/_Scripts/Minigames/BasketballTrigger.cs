using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;

public class BasketballTrigger : MonoBehaviour
{
    public VRTK_InteractableObject interactableObject;
    private void Update()
    {
        if (interactableObject.IsTouched())
        {
            SceneManager.LoadScene("MinigameOne", LoadSceneMode.Additive);
        }
    }
}

