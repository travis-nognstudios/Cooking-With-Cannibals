using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cooking
{
    public class Cookable : MonoBehaviour
    {
        #region Variables

        [Header("Type")]
        public Ingredient ingredientType;

        [Header("Cooking Settings")]
        public Cooktype cooktype;
        public float timeToCook;
        public float timeToBurn;

        private float timeCooked;
        private Cookstate cookstate;

        [Header("Textures")]
        public Material uncookedMat;
        public Material cookedMat;
        public Material burntMat;
        Renderer rend;

        #endregion Variables


        #region Properties

        /// <summary>
        /// Whether the object is raw
        /// </summary>
        /// <returns></returns>
        public bool IsRaw()
        {
            return cookstate == Cookstate.Uncooked;
        }

        /// <summary>
        /// Whether the object is cooked
        /// </summary>
        /// <returns></returns>
        public bool IsCooked()
        {
            return cookstate == Cookstate.Cooked;
        }

        /// <summary>
        /// Whether the object is burnt
        /// </summary>
        /// <returns></returns>
        public bool IsBurnt()
        {
            return cookstate == Cookstate.Burnt;
        }

        #endregion Properties


        #region MonoBehavior
        // Start is called before the first frame update
        void Start()
        {
            rend = GetComponent<Renderer>();
            rend.enabled = true;
            rend.sharedMaterial = uncookedMat;
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerStay(Collider other)
        {
            //ToDo: Add a check that cooktop's cooktype matches my cooktype
            //Removing it for the demo
            if (other.gameObject.CompareTag("Cooktop"))
            {
                Cooktop cooktop = other.gameObject.GetComponent<Cooktop>();

                if (cooktop.cooktype == cooktype && cooktop.IsHot())
                {
                    timeCooked += Time.deltaTime;

                    if (!IsCooked() && timeCooked >= timeToCook && timeCooked < timeToBurn)
                    {
                        MakeCooked();
                    }
                    else if (!IsBurnt() && timeCooked >= timeToBurn)
                    {
                        MakeBurnt();
                    }
                }
            }
        }
        #endregion MonoBehavior

        #region Private Methods
        private void MakeCooked()
        {
            cookstate = Cookstate.Cooked;
            rend.sharedMaterial = cookedMat;
            //Debug.Log("Cooked");
        }

        private void MakeBurnt()
        {
            cookstate = Cookstate.Burnt;
            rend.sharedMaterial = burntMat;
            //Debug.Log("Burnt");
        }
        #endregion Private Methods

    }
}
