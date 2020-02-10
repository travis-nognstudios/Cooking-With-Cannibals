using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;
public class LevelManager : Singleton<LevelManager>
{
    //public VRTK_HeadsetFade headsetFade;
    public OVRScreenFade screenFade;
    public Color color = new Color(0,0,0,1);
    private bool loading = false;

    private void Start()
    {
        loading = false;
    }
    public void LoadNextScene()
    {
        if (!loading)
        {
            loading = true;
            screenFade.FadeOut();
            StartCoroutine(LoadNextLevel());
        }
    }

    public void LoadScene(int i)
    {
        if (!loading)
        {
            loading = true;
            screenFade.FadeOut();
            StartCoroutine(LoadLevel(i));
        }



    }
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(screenFade.fadeTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator LoadLevel(int i)
    {
        yield return new WaitForSeconds(screenFade.fadeTime);
        SceneManager.LoadScene(i);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
