using UnityEngine;
using System.Collections;

namespace SceneObjects
{
    public class Gyroscope : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            transform.rotation = Quaternion.identity;
        }
    }
}