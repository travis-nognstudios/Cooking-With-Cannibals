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
        
        void Start()
        {
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            if (startedOpening && !isOpen)
            {
                timer += Time.deltaTime;
                if (timer >= openTime)
                {
                    isOpen = true;
                }
            }
        }

        public void OpenAwning()
        {
            animator.SetTrigger("open");
            startedOpening = true;
        }

        public void DoneOpening()
        {
            isOpen = true;
        }
    }
}