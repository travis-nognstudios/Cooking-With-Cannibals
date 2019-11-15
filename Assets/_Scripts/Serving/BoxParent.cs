using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxParent : MonoBehaviour
{
    public int i = 0;
    public void BoxClosed(BoxClose child)
    {
        gameObject.SendMessage("SpawnMeal", i);
    }
}
