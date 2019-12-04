using UnityEngine;

public class BoxClose : MonoBehaviour
{
    [HideInInspector]
    public bool isClosed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Styrofoam_Top")
        {
            isClosed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Styrofoam_Top")
        {
            isClosed = false;
        }
    }
}
