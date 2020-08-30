using UnityEngine;

namespace SceneObjects
{
    public class SodaBottlePour : MonoBehaviour
    {
        public ParticleSystem pourEffect;
        private Rigidbody rb;

        private float pourMinAngle = 90f;
        private float pourMaxAngle = 270f;

        // Use this for initialization
        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            ControlPour();
        }

        bool IsPouring()
        {
            float x = transform.rotation.eulerAngles.x;
            float z = transform.rotation.eulerAngles.z;

            return IsInPourRange(x) || IsInPourRange(z);
        }

        bool IsInPourRange(float rotation)
        {
            if (rotation > pourMinAngle && rotation < pourMaxAngle)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        void ControlPour()
        {
            if (IsPouring() && !pourEffect.isPlaying)
            {
                pourEffect.Play();
            }
            else if (!IsPouring() && pourEffect.isPlaying)
            {
                pourEffect.Stop();
            }
        }
    }
}