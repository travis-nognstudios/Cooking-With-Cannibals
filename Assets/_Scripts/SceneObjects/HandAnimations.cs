using UnityEngine;
using System.Collections;

namespace SceneObjects
{
    public class HandAnimations : MonoBehaviour
    {
        public Animator animator;

        // Use this for initialization
        void Start()
        {
            float rn = Random.Range(0, 100);

            if (rn <= 50)
            {
                PlayYEET();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void PlayYEET()
        {
            animator.SetTrigger("triggerYEET");
        }

        public void PlaySOS()
        {
            animator.SetTrigger("triggerSOS");
        }
    }
}