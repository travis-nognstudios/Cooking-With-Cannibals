using System.Collections.Generic;
using UnityEngine;

namespace Serving
{
    public class MealArea : MonoBehaviour
    {
        [Header("Area")]
        public Collider foodArea;
        public Transform spawnPoint;

        [Header("Timers")]
        public float spawnedMealDestroyTime = 10f;
        public float spawnerCooldownTime = 1f;

        [Header("Tracking")]
        public string mainIngredientName;

        private List<GameObject> inFoodArea = new List<GameObject>();

        private float spawnerCooldown;
        private bool spawnerOnCooldown;

        void Start()
        {
            
        }

        void Update()
        {
            TimerSpawnerCooldown();
        }

        private void TimerSpawnerCooldown()
        {
            if (spawnerOnCooldown)
            {
                spawnerCooldown += Time.deltaTime;
                if (spawnerCooldown >= spawnerCooldownTime)
                {
                    StopSpawnerCooldown();
                }
            }
        }

        private GameObject Spawn(GameObject item)
        {
            GameObject spawnedItem = Instantiate(item, spawnPoint.position, item.transform.rotation);
            // FinishedMeal finishedMeal = spawnedMeal.GetComponent<FinishedMeal>();
            // finishedMeal.PlayFinishFX();

            return spawnedItem;
        }

        public void DespawnIngredients()
        {
            foreach (GameObject item in inFoodArea)
            {
                if (item != null)
                {
                    Destroy(item);
                }
            }

            inFoodArea.Clear();
        }

        private void GetInFoodAreaItems()
        {
            inFoodArea.Clear();

            Vector3 center = foodArea.bounds.center;
            Vector3 size = foodArea.bounds.size;
            Vector3 halfSize = new Vector3(size[0] / 2, size[1] / 2, size[2] / 2);
            Quaternion orientation = foodArea.gameObject.transform.rotation;
            LayerMask foodLayer = LayerMask.GetMask("Default");

            Collider[] collidersInBox = Physics.OverlapBox(center, halfSize, orientation, foodLayer);
            foreach (Collider c in collidersInBox)
            {
                GameObject ingredient = c.gameObject;

                if (!inFoodArea.Contains(ingredient))
                {
                    inFoodArea.Add(ingredient);
                }
            }
        }

        public List<string> GetInFoodAreaNames()
        {
            List<string> inFoodAreaNames = new List<string>();
            foreach (GameObject item in inFoodArea)
            {
                inFoodAreaNames.Add(item.name);
            }

            return inFoodAreaNames;
        }

        private void StartSpawnerCooldown()
        {
            spawnerOnCooldown = true;
        }

        private void StopSpawnerCooldown()
        {
            spawnerOnCooldown = false;
            spawnerCooldown = 0f;
        }
    }
}