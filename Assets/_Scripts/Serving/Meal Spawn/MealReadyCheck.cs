using UnityEngine;

namespace Serving
{
    public abstract class MealReadyCheck : MonoBehaviour
    {
        public bool isReady;

        public abstract void MakeReady();

        public abstract void MakeNotReady();

        public bool IsReady()
        {
            return isReady;
        }
    }
}