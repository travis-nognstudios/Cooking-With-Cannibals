 using UnityEngine;
using System.Collections;

public class Cutter : MonoBehaviour {

    private bool isColliding = false;

    private void OnTriggerEnter(Collider collision)
    {
        if (isColliding) return;
        isColliding = true;
        if (collision.gameObject.CompareTag("Cuttable"))
        {
            collision.gameObject.GetComponent<Cutting>().Chop();
        }

        StartCoroutine(Reset());
    }

    IEnumerator Reset()
    {
        yield return new WaitForEndOfFrame();
        isColliding = false;
    }

}
