using UnityEngine;
using System.Collections.Generic;

namespace Liquor
{
    public class Glass : MonoBehaviour
    {
        public bool hasLiquid;
        public List<string> contains = new List<string>();

        public void AddToGlass(string itemName)
        {
            contains.Add(itemName);
        }
    }
}