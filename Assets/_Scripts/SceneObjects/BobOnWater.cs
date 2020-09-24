using UnityEngine;
using System.Collections;

namespace SceneObjects
{
    public class BobOnWater : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {
            GetComponent<Rigidbody>().sleepThreshold = 0f;
        }
    }
}