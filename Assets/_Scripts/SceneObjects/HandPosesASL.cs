using UnityEngine;
using System.Collections;

namespace SceneObjects
{
    public class HandPosesASL : MonoBehaviour
    {
        [System.Serializable]
        [HideInInspector]
        public enum ASLPose
        { 
            A,
            B,
            C,
            D,
            E,
            F,
            G,
            H,
            I,
            K,
            L,
            M,
            N,
            O,
            P,
            R,
            S,
            T,
            U,
            V,
            W,
            Y
        }

        public ASLPose[] poses;
        private SkinnedMeshRenderer rend;

        // Use this for initialization
        void Start()
        {
            rend = GetComponent<SkinnedMeshRenderer>();

            int numPoses = poses.Length;
            int randomNumber = Random.Range(0, 100);
            int selectedPose = randomNumber % numPoses;
            int blendIndex = (int) poses[selectedPose];

            rend.SetBlendShapeWeight(blendIndex, 100);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}