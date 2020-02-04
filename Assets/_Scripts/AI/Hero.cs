using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class Hero : MonoBehaviour
    {
        public Animator animator;

        private float fastThrowSpeed = 0.2f;

        private Collision objectStuckOnHero;
        private bool isPointing;

        public AudioSource myAudioSource;
        public AudioClip jokeClip;
        private bool toldJoke;

        void Start()
        {
            //animator = GetComponent<Animator>();
        }

        void Update()
        {

        }

        public void PointToItem(GameObject item)
        {
            isPointing = true;
            Vector3 itemPos = item.transform.position;
            Debug.Log($"Bruce: I'm pointing at {item.name} which is at {itemPos.ToString()}");
        }

        public void StopPointing()
        {
            isPointing = false;
        }

        public void TellAJoke()
        {
            myAudioSource.clip = jokeClip;
            myAudioSource.Play();
        }

        public void ReactToThrownItem()
        {
            //animator.SetTrigger("reactToThrownItem");
            Debug.Log("Bruce: You threw an item at me!");
        }

        public void ReactToThrownKnife()
        {
            //animator.setTrigger("reactToThrownKnife");
            Debug.Log("Bruce: You threw a knife at me!");

        }

        private void FreezeObjectOnHero(Collision collision)
        {
            objectStuckOnHero = collision;
            objectStuckOnHero.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }

        private void UnFreezeObjectOnHero()
        {
            objectStuckOnHero.rigidbody.constraints = RigidbodyConstraints.None;
        }

        private void OnCollisionEnter(Collision collision)
        {
            GameObject thrownObject = collision.gameObject;
            string objectName = thrownObject.name;
            float objectSpeed = collision.rigidbody.velocity.magnitude;

            if (objectName.Contains("Knife") || objectName.Contains("knife"))
            {
                //FreezeObjectOnHero(collision);
                ReactToThrownKnife();
            }
            else if (objectSpeed > fastThrowSpeed)
            {
                ReactToThrownItem();
            }
        }
    }
}