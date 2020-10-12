using UnityEngine;
using System.Collections;

namespace Liquor
{
    public class Garnish : MonoBehaviour
    {
        public GameObject garnish;
        public string filledBy;
        public Glass glass;

        private bool alreadyAdded;

        private void OnCollisionEnter(Collision collision)
        {
            if (!alreadyAdded && collision.gameObject.CompareTag("Garnish"))
            {
                string name = collision.gameObject.name;
                bool isCorrectGarnish = name.StartsWith(filledBy);
                if (isCorrectGarnish)
                {
                    AddGarnish(collision);
                }
            }
        }

        private void AddGarnish(Collision garnishCollision)
        {
            garnish.SetActive(true);
            Destroy(garnishCollision.gameObject);

            glass.AddToGlass(filledBy);
            alreadyAdded = true;
        }

    }
}