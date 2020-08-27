using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosterDissolve : MonoBehaviour
{
    public GameObject[] faces;

    public float dissolveRate = 0.01f;
    public bool isDissolving;

    private string shaderProperty = "Vector1_FC4AF8F5";
    private float fullDissolveValue = 1.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isDissolving)
        {
            MakeDissolve();
        }
    }

    void MakeDissolve()
    {
        foreach (GameObject face in faces)
        {
            Renderer rend = GetComponent<Renderer>();
            MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
            rend.GetPropertyBlock(propBlock);

            float currentValue = propBlock.GetFloat(shaderProperty);

            if (currentValue < fullDissolveValue)
            {
                float newValue = currentValue += dissolveRate;
                propBlock.SetFloat(shaderProperty, newValue);
                rend.SetPropertyBlock(propBlock);
            }
            else
            {
                isDissolving = false;
            }
        }
    }
  
}
