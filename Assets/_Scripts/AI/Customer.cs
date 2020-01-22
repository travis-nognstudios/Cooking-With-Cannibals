using UnityEngine;

namespace AI
{
    public class Customer : MonoBehaviour
    {
        public Animator animator;

        private Vector3 startPosition;
        private Vector3 endPosition;

        public GameObject orderingPoint;

        void Start()
        {
            animator = GetComponent<Animator>();
            startPosition = transform.position;
            endPosition = startPosition;
        }

        void Update()
        {

        }

        public void GoToOrderingPosition()
        {
            Vector3 current = transform.position;
            Vector3 target = orderingPoint.transform.position;

            Vector3 newPos = new Vector3(target.x, current.y, target.z);
            transform.position = newPos;

        }

        public void GoToEndPosition()
        {
            Vector3 current = transform.position;
            Vector3 target = endPosition;

            Vector3 newPos = new Vector3(target.x, current.y, target.z);
            transform.position = newPos;
        }
    }
}