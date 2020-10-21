using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

namespace LevelManagement
{
    // TODO: Refactor!

    public class PauseManagerv2 : MonoBehaviour
    {
        [Header("Pause Configuration")]
        public bool pausable;
        public bool isPaused;
        public float fadeTime = 2;
        public float pauseCooldown;
        public PostProcessLayer pauseNoir;

        [Header("Player")]
        public GameObject player;
        public Transform pausePoint;
        public Transform easterEggPoint;
        public Transform minigamePoint;
        private Transform goToPoint;

        private Vector3 originalPosition;
        private Quaternion originalRotation;

        private OVRScreenFade screenFade;
        private float fadeTimer;
        private bool isFading;
        private bool fadeReady;
        private bool keyboardMode;

        private bool waitingToTeleport;
        private bool teleportingToPause;
        private bool teleportingBack;

        private bool waitingToSceneSwitch;
        private int nextSceneIndex;

        private float pauseCooldownTimer;
        private bool pauseOnCooldown;

        // Use this for initialization
        void Start()
        {
            //ColorOn();
            SetLocationToPauseArea();
        }

        // Update is called once per frame
        void Update()
        {
            if (isFading)
            {
                fadeTimer += Time.deltaTime;
                if (fadeTimer >= fadeTime)
                {
                    isFading = false;
                    fadeReady = true;
                    fadeTimer = 0;
                }
            }

            if (waitingToTeleport && fadeReady)
            {
                waitingToTeleport = false;

                if (teleportingToPause)
                {
                    TeleportToPause();
                }
                else if (teleportingBack)
                {
                    TeleportBackToLevel();
                }
            }

            if (pauseOnCooldown)
            {
                pauseCooldownTimer += Time.deltaTime;
                if (pauseCooldownTimer >= pauseCooldown)
                {
                    pauseOnCooldown = false;
                }
            }

            if (waitingToSceneSwitch && fadeReady)
            {
                SwitchScene();
            }
        }

        public void SetLocationToPauseArea()
        {
            goToPoint = pausePoint;
        }

        public void SetLocationToEasterEgg()
        {
            goToPoint = easterEggPoint;
        }

        public void SetLocationToMinigame()
        {
            goToPoint = minigamePoint;
        }

        public void SetPause()
        {
            if (pausable && !isPaused && !pauseOnCooldown)
            {
                if (goToPoint != minigamePoint)
                {
                    isPaused = true;
                    pauseOnCooldown = true;
                }
                FadeOut();
                StartTeleportToPause();
            }
        }

        public void SetUnpause()
        {
            if (pausable && isPaused && !pauseOnCooldown)
            {
                isPaused = false;
                pauseOnCooldown = true;
                FadeOut();
                StartTeleportBackToLevel();
            }
        }

        public void SetNextScene()
        {
            nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            FadeOut();
            StartNextScene();
        }

        public void FadeOut()
        {
            if (screenFade == null)
            {
                screenFade = FindObjectOfType<OVRScreenFade>();

                if (screenFade == null)
                {
                    keyboardMode = true;
                }
                else
                {
                    screenFade.fadeTime = fadeTime / 2;
                }
            }

            fadeReady = false;
            isFading = true;

            if (!keyboardMode)
            {
                screenFade.FadeOut();
            }
        }

        public float DeltaTime()
        {
            if (isPaused)
            {
                return 0f;
            }
            else
            {
                return Time.deltaTime;
            }
        }

        public void StartTeleportToPause()
        {
            waitingToTeleport = true;
            teleportingToPause = true;
            teleportingBack = false;
        }

        private void StartTeleportBackToLevel()
        {
            waitingToTeleport = true;
            teleportingToPause = false;
            teleportingBack = true;
        }

        private void StartNextScene()
        {
            waitingToSceneSwitch = true;
        }

        private void TeleportToPause()
        {
            if (goToPoint == pausePoint)
            {
                //ColorOff();
            }

            float x = player.transform.position.x;
            float y = player.transform.position.y;
            float z = player.transform.position.z;

            originalPosition = new Vector3(x,y,z);
            originalRotation = player.transform.rotation;

            player.transform.position = goToPoint.position;
            player.transform.rotation = goToPoint.rotation;

            if (!keyboardMode)
            {
                screenFade.UnFade();
            }
        }

        private void TeleportBackToLevel()
        {
            //ColorOn();

            player.transform.position = originalPosition;
            player.transform.rotation = originalRotation;

            if (!keyboardMode)
            {
                screenFade.UnFade();
            }
        }

        private void SwitchScene()
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        
        public void JumpToScene(int n)
        {
            nextSceneIndex = n;
            FadeOut();
            StartNextScene();
        }

        private void ColorOn()
        {
            if (!keyboardMode)
            {
                pauseNoir.enabled = false;
            }
        }

        private void ColorOff()
        {
            if (!keyboardMode)
            {
                pauseNoir.enabled = true;
            }
        }
    }
}