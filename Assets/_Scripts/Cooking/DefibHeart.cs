using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cooking
{
    
	public class DefibHeart : MonoBehaviour
    {
        public ParticleSystem[] spark;
		public int length = 6;
		/*public int length = spark.Length;
		
		public void Start()
		{
			for(int i=0; i<length; i++)
			{
				spark[i].Stop();
			}
			
		}
		*/
        public void OnTriggerEnter(Collider other)
        {
			for(int i=0; i<length; i++)
					{
						spark[i].Play();
					}
            if (other.CompareTag("Heart"))
            {
                if (other.GetComponent< Cookablev2 >())
                {
                    
                    other.GetComponent<Cookablev2>().MakeCooked(CookType.Grill);
                    
                }
            }
        }
		
		public void OnTriggerExit(Collider other)
        {
			for(int i=0; i<length; i++)
					{
						spark[i].Stop();
					}
		}

    }
}
