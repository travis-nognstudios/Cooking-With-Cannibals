using System;

namespace Cooking
{
    [System.Serializable]
    public struct CookTime
    {
        public CookType cookType;
        public float timeToCook;
        public float timeToOverCook;
    }
}