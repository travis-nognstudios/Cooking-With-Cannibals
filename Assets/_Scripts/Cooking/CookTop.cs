using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneObjects;

namespace Cooking
{
    public class CookTop : MonoBehaviour
    {
        #region Variables

        private bool hot;

        [Header("Cooktop Settings")]
        public CookType cookType;

        #endregion Variables

        #region MonoBehavior
        // Start is called before the first frame update
        void Start()
        {
            MakeCold();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Heatsource"))
            {
                MakeHot();
            }

            if (other.CompareTag("Hand"))
            {
                other.gameObject.GetComponent<HandAnimations>().PlaySOS();
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Heatsource"))
            {
                MakeCold();
            }
        }

        #endregion MonoBehavior

        #region Private Methods
        private void MakeHot()
        {
            hot = true;
            //Debug.Log("Now hot");
        }

        private void MakeCold()
        {
            hot = false;
            //Debug.Log("Now cold");
        }

        #endregion Private Methods

        #region Properties
        public bool IsHot()
        {
            return hot;
        }
        #endregion Properties
    }
}