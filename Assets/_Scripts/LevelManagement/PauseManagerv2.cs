using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.PostProcessing;

namespace LevelManagement
{
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

        private Vector3 originalPosition;
        private Quaternion originalRotation;

        private OVRScreenFade screenFade;
        private float fadeTimer;
        private bool isFading;
        private bool fadeReady;

        private bool waitingToTeleport;
        private bool teleportingToPause;
        private bool teleportingBack;

        private float pauseCooldownTimer;
        private bool pauseOnCooldown;

        // Use this for initialization
        void Start()
        {
            ColorOn();
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
        }

        public void SetPause()
        {
            if (pausable && !isPaused && !pauseOnCooldown)
            {
                isPaused = true;
                pauseOnCooldown = true;
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

        public void FadeOut()
        {
            if (screenFade == null)
            {
                screenFade = FindObjectOfType<OVRScreenFade>();
                screenFade.fadeTime = fadeTime / 2;
            }

            fadeReady = false;
            isFading = true;
            screenFade.FadeOut();
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

        private void StartTeleportToPause()
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

        private void TeleportToPause()
        {
            ColorOff();
            float x = player.transform.position.x;
            float y = player.transform.position.y;
            float z = player.transform.position.z;

            originalPosition = new Vector3(x,y,z);
            originalRotation = player.transform.rotation;


            player.transform.position = pausePoint.position;
            player.transform.rotation = pausePoint.rotation;

            screenFade.UnFade();
        }

        private void TeleportBackToLevel()
        {
            ColorOn();

            player.transform.position = originalPosition;
            player.transform.rotation = originalRotation;

            screenFade.UnFade();
        }

        private void ColorOn()
        {
            pauseNoir.enabled = false;
        }

        private void ColorOff()
        {
            pauseNoir.enabled = true;
        }
    }
}