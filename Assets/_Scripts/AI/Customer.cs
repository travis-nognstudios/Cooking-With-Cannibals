using UnityEngine;

namespace AI
{
    public class Customer : MonoBehaviour
    {
        public Animator animator;

        private Vector3 startPosition;
        private Vector3 endPosition;

        public Vector3 orderingPosition;

        void Start()
        {
            //animator = GetComponent<Animator>();
            startPosition = transform.position;
            endPosition = startPosition;
        }

        void Update()
        {

        }

        public void GoToOrderingPosition()
        {
            transform.position = orderingPosition;
        }

        public void GoToEndPosition()
        {
            transform.position = endPosition;
        }
    }
}