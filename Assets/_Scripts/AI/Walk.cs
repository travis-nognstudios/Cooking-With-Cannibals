using UnityEngine;
using System.Collections;

namespace AI
{
    public class Walk : MonoBehaviour
    {
        public float walkSpeed = 1f;
        public float turnSpeed = Mathf.PI * 2;

        private float stepSize = 0.1f;
        private Vector3 destination;

        private bool atDestination;
        private bool turning;
        private bool walking;

        void Start()
        {

        }

        void Update()
        {
            if (turning)
            {
                Turn();
                CheckFacing();
            }

            if (walking)
            {
                Move();
                CheckReached();
            }
        }

        public void To(Vector3 destination)
        {
            this.destination = new Vector3(destination.x, transform.position.y, destination.z);
            turning = true;
            walking = true;
        }

        private void Turn()
        {
            Vector3 direction = destination - transform.position;
            float turnRadius = turnSpeed * Time.deltaTime;

            Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, turnRadius, 0);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }

        private void Move()
        {
            float step = walkSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, destination, step);
        }

        private void CheckFacing()
        {
            Vector3 direction = destination - transform.position;
            float angle = Vector3.Angle(transform.forward, direction);

            bool facing = angle < 0.5;
            if (facing)
            {
                turning = false;
            }
        }

        private void CheckReached()
        {
            bool reached = Vector3.Distance(destination, transform.position) < stepSize;
            if (reached)
            {
                walking = false;
            }
        }

        public bool IsAtDestination()
        {
            return atDestination;
        }
    }
}