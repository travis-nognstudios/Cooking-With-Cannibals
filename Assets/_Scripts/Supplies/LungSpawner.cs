using UnityEngine;

namespace Supplies
{
    [System.Serializable]
    public class LungSpawner : MonoBehaviour
    {
        [Header("Attached Lungs")]
        public GameObject leftLung;
        public GameObject rightLung;

        [Header("Template Lungs")]
        public GameObject leftLungTemplate;
        public GameObject rightLungTemplate;

        [Header("Timing")]
        public float spawnInterval = 3;
        private float randomDelay = 0.5f;
        private float timer;

        private void Start()
        {
            
        }

        private void Update()
        {
            timer += Time.deltaTime;

            if (timer + randomDelay > spawnInterval)
            {
                CheckLungs();
                timer = 0;
                randomDelay = Random.Range(0.1f, 1f);
            }
        }

        private void CheckLungs()
        {
            SpringJoint leftLungJoint = leftLung.GetComponent<SpringJoint>();
            bool leftLungConnected = leftLungJoint != null;

            SpringJoint rightLungJoint = rightLung.GetComponent<SpringJoint>();
            bool rightLungConnected = rightLungJoint != null;

            if (!leftLungConnected)
            {
                GameObject newLeftLung = SpawnLung(leftLungTemplate);
                leftLung = newLeftLung;
            }

            if (!rightLungConnected)
            {
                GameObject newRightLung = SpawnLung(rightLungTemplate);
                rightLung = newRightLung;
            }
        }

        private GameObject SpawnLung(GameObject templateLung)
        {
            GameObject newLung = Instantiate(templateLung, templateLung.transform.position, templateLung.transform.rotation);
            SpringJoint newLungJoint = newLung.GetComponent<SpringJoint>();
            Rigidbody myRigidbody = GetComponent<Rigidbody>();
            newLungJoint.connectedBody = myRigidbody;
            newLung.SetActive(true);

            return newLung;
        }
    }
}