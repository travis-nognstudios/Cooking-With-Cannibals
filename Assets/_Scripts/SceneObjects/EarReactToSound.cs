using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SceneObjects
{
    public class EarReactToSound : MonoBehaviour
    {
        [Range(0.1f, 5f)]
        public float listeningRange = 0.5f;
        public bool alwaysDance = false;
        public Animator animator;

        private void Update()
        {
            AudioSource source = GetNearbyNoiseSource();
            if (source != null)
            {
                ReactToNoise();
            }
        }

        private AudioSource GetNearbyNoiseSource()
        {
            Collider[] objectsInRange = Physics.OverlapSphere(transform.position, listeningRange);

            if (objectsInRange.Length > 0)
            {
                for (int i = 0; i < objectsInRange.Length; ++i)
                {
                    GameObject obj = objectsInRange[i].gameObject;
                    AudioSource audioSource = obj.GetComponent<AudioSource>();

                    if (audioSource != null && audioSource.isPlaying)
                    {
                        return audioSource;
                    }
                }
            }

            return null;
        }

        private void ReactToNoise()
        {
            float diceroll = Random.Range(0f, 100f);
            if (alwaysDance) diceroll = 0;

            if (diceroll < 100 * Time.deltaTime)
            {
                animator.SetTrigger("Dance");
            }
        }
    }
}
