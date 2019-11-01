using UnityEngine;
using System.Collections;

namespace Cooking
{
    [System.Serializable]
    public struct CookableIngredient
    {
        public GameObject gameObject;
        public CookMechanic[] steps;
    }
}