using UnityEngine;
using System.Collections;

namespace SceneObjects
{
    public class Gyroscope : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            transform.rotation = Quaternion.identity;
        }
    }
}