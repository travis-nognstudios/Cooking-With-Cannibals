using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;
public class LevelManager : Singleton<LevelManager>
{
    public VRTK_HeadsetFade headsetFade;
    public OVRScreenFade screenFade;
    public Color color = new Color(0,0,0,1);

    public void LoadNextScene()
    {
        screenFade.FadeOut();
        StartCoroutine(LoadLevel());

        
        
    }

    IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(screenFade.fadeTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
