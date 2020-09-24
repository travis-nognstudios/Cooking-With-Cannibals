using System;
using UnityEngine;
using AI;

namespace Serving
{
    [System.Serializable]
    public class RatingCardPoint : MonoBehaviour
    {
        private GameObject cardReference;

        public void SetCard(GameObject card)
        {
            // Instantiate card object
            // Create spring joint to spawn point
            Vector3 offset = new Vector3(0, -0.1f, 0);
            Vector3 position = transform.position + offset;
            Quaternion rotation = card.transform.rotation;

            GameObject createdCard = Instantiate(card, position, rotation);
            createdCard.GetComponent<SpringJoint>().connectedBody = GetComponent<Rigidbody>();

            cardReference = createdCard;
        }

        public void DestroyCard()
        {
            if (cardReference != null)
            {
                Destroy(cardReference);
            }
        }

    }
}
