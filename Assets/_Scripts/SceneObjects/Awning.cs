using UnityEngine;

namespace SceneObjects
{
    public class Awning : MonoBehaviour
    {
        private bool startedOpening;
        [HideInInspector]
        public bool isOpen;

        public float openTime = 2.5f;
        private float timer;

        Animator animator;
        public AudioSource audioSource;
        
        void Start()
        {
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            if (startedOpening && !isOpen)
            {
                timer += Time.deltaTime;
                if (timer >= openTime)
                {
                    isOpen = true;
                    audioSource.Stop();
                }
            }
        }

        public void OpenAwning()
        {
            animator.SetTrigger("open");
            startedOpening = true;
            this.audioSource.Play();
        }

        public void DoneOpening()
        {
            isOpen = true;
            this.audioSource.Stop();
        }
    }
}