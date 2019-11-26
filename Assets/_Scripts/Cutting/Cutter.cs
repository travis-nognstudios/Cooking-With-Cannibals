 using UnityEngine;
using System.Collections;

public class Cutter : MonoBehaviour {

    private bool isColliding = false;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<Cutting>() && !isColliding)
        {
            isColliding = true;
            collision.gameObject.GetComponent<Cutting>().Chop();
            isColliding = false;
        }

    }


}
