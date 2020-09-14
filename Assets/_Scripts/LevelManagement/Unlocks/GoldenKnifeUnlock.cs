using UnityEngine;
using System.Collections;

namespace LevelManagement
{
    public class GoldenKnifeUnlock : MonoBehaviour
    {
        public GameObject regularKnife;
        public GameObject goldenKnife;

        // Use this for initialization
        void Start()
        {
            if (GoldenKnifeUnlocked())
            {
                regularKnife.SetActive(false);
                goldenKnife.SetActive(true);
            }
            else
            {
                regularKnife.SetActive(true);
                goldenKnife.SetActive(false);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        bool GoldenKnifeUnlocked()
        {
            return SaveState.goldKnifeUnlocked;

            /* For Testing */
            //return true;
        }
    }
}