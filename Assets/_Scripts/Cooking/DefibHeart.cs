using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cooking
{
    
	public class DefibHeart : MonoBehaviour
    {
		public GameObject sparks;

		
		public void Start()
		{
			sparks.SetActive(false);
	
		}
		
        public void OnTriggerEnter(Collider other)
        {
			
            if (other.CompareTag("Heart"))
            {
				sparks.SetActive(true);
				
                if (other.GetComponent< Cookablev2 >())
                {
                    other.GetComponent<Cookablev2>().MakeCooked(CookType.Grill);  
                }
            }
        }
		
		public void OnTriggerExit(Collider other)
        {
			sparks.SetActive(false);
		}
		
    }
}
