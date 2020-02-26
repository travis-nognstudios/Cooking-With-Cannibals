using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;

namespace LevelManagement
{
    public class LevelManager : Singleton<LevelManager>
    {
        public OVRScreenFade screenFade;
        public Color color = new Color(0, 0, 0, 1);

        private bool loading = false;
        public GameObject VRTK_SDKSetup;
        public GameObject pauseMenuSpawn;

        public float pauseCooldown;

        private Vector3 origPosition;
        private void Start()
        {
            loading = false;
            //SceneManager.LoadScene(0, LoadSceneMode.Additive);
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
                PauseManager.Instance.TogglePause();
                loading = true;
                screenFade.FadeOut();
                StartCoroutine(LoadLevel());
            }
        }

        IEnumerator LoadNextLevel()
        {
            yield return new WaitForSeconds(screenFade.fadeTime);
            loading = false;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        IEnumerator LoadLevel()
        {
            yield return new WaitForSeconds(screenFade.fadeTime);

            if (!PauseManager.Instance.Paused)
                PressedPlay();
            else
                PressedPause();

            screenFade.UnFade();
            yield return new WaitForSeconds(pauseCooldown);


            loading = false;
            //SceneManager.LoadScene(i);
        }

        // Update is called once per frame
        void Update()
        {
            screenFade = FindObjectOfType<OVRScreenFade>();
        }

        public void PressedPause()
        {
            origPosition = VRTK_SDKSetup.transform.position;
            VRTK_SDKSetup.transform.localPosition = pauseMenuSpawn.transform.position;
        }

        public void PressedPlay()
        {
            VRTK_SDKSetup.transform.localPosition = origPosition;
        }

    }
}