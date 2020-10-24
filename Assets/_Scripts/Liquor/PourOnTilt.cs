using System;
using UnityEngine;

namespace Liquor
{
    class PourOnTilt : MonoBehaviour
    {
        public ParticleSystem pourFX;
        public GameObject pourArea;

        private readonly float pourMinAngle = 90f;
        private readonly float pourMaxAngle = 270f;

        void Update()
        {
            ControlPour();
        }

        void ControlPour()
        {
            if (IsPouring() && !pourFX.isPlaying)
            {
                pourFX.Play();
                pourArea.SetActive(true);
            }
            else if ((!IsPouring() && pourFX.isPlaying))
            {
                pourFX.Stop();
                pourArea.SetActive(false);
            }
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
    }
}
