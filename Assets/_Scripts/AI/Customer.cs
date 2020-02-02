using UnityEngine;

namespace AI
{
    public class Customer : MonoBehaviour
    {
        public Animator animator;

        private Vector3 startPosition;
        private Vector3 endPosition;

        void Start()
        {
            animator = GetComponent<Animator>();
            startPosition = transform.position;
            endPosition = startPosition;
        }

        void Update()
        {

        }

        public void GoToOrderingPosition(GameObject orderingPoint)
        {
            Vector3 current = transform.position;
            Vector3 target = orderingPoint.transform.position;

            Vector3 newPos = new Vector3(target.x, current.y, target.z);
            GoTo(newPos);

        }

        public void GoToEndPosition()
        {
            Vector3 current = transform.position;
            Vector3 target = endPosition;

            Vector3 newPos = new Vector3(target.x, current.y, target.z);
            GoTo(newPos);
        }

        public void GoTo(Vector3 newPos)
        {
            Walk walk = GetComponent<Walk>();
            walk.To(newPos);
        }
    }
}