using System.Collections;

using UnityEngine;

namespace LevelManagement
{
    public class PauseManager : Singleton<PauseManager>
    {
        [Header("Pause Configuration")]
        public bool pausable = true;
        public bool paused = false;

        // Whether pause was toggled this frame
        bool toggledPauseThisFrame;

        public float deltaTime;

        public bool Pausable
        {
            get { return pausable; }
            set
            {
                pausable = value;
                ToggledPauseThisFrame = true;
            }
        }

        public bool Paused
        {
            get { return paused || toggledPauseThisFrame; }
            set
            {
                if (pausable)
                {
                    // Check that the value of paused is being changed
                    // This is to prevent events like OnPause and OnResume from firing when the game is already in the state that one of those events indicates
                    if (paused != value)
                    {
                        paused = value;

                        ToggledPauseThisFrame = true;

                        // Check if the game is now paused but wasn't before
                        if (paused)
                        {
                            // Check for any listeners to the OnPause event
                            // If any are found, call OnPause
                            OnPause?.Invoke();
                        }
                        // The game has been resumed
                        else
                        {
                            // Check for any listeners to the OnResume event
                            // If any are found, call OnResume
                            OnResume?.Invoke();
                        }

                        StartCoroutine(WaitOneFrameAfterPause());
                    }
                }
            }
        }

        public bool ToggledPauseThisFrame
        {
            get { return toggledPauseThisFrame; }
            set
            {
                if (toggledPauseThisFrame != value)
                {
                    toggledPauseThisFrame = value;

                    if (toggledPauseThisFrame)
                    {
                        StartCoroutine(WaitOneFrameAfterPause());
                    }
                }
            }
        }

        #region Events
        /// <summary>
        /// Function that listens to when the game is paused
        /// </summary>
        public delegate void PauseEventHandler();

        /// <summary>
        /// Broadcasts that the game has been resumed
        /// </summary>
        public event PauseEventHandler OnPause;

        /// <summary>
        /// Function that listens to when the game is resumed
        /// </summary>
        public delegate void ResumeEventHandler();

        /// <summary>
        /// Broadcasts that the game has been resumed
        /// </summary>
        public event ResumeEventHandler OnResume;
        #endregion Events

        #region MonoBehaviour
        private void Update()
        {
            if (Paused)
            {
                deltaTime = 0;
            }
            else
            {
                deltaTime = Time.deltaTime;
            }
        }
        #endregion MonoBehaviour

        public void TogglePause()
        {

            Paused = !paused;

        }

        private void TryPause()
        {
            if (!toggledPauseThisFrame)
            {
                TogglePause();
            }
        }
        private IEnumerator WaitOneFrameAfterPause()
        {
            yield return null;

            toggledPauseThisFrame = false;
        }
    }
}