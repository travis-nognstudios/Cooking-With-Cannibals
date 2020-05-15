using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelManagement;
public class MinigameTeleport : MonoBehaviour
{
    public PauseManagerv2 pauseManager;
    public MinigameManager minigameManager;
    public void Teleport()
    {
        pauseManager.SetLocationToMinigame();
        pauseManager.SetPause();
        StartCoroutine(PlayMini());
    }

    IEnumerator PlayMini()
    {
        yield return new WaitForSeconds(5f);
        minigameManager.Play();
    }

}
