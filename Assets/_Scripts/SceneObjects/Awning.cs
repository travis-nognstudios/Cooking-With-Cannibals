using UnityEngine;

namespace SceneObjects
{
    public class Awning : MonoBehaviour
    {
        public bool isOpen;
        
        void Start()
        {

        }

        void Update()
        {

        }

        public void OpenAwning()
        {
            Debug.Log("Opening Awning");
            isOpen = true;
        }

        public void CloseAwning()
        {
            Debug.Log("Closing Awning");
            isOpen = false;
        }
    }
}