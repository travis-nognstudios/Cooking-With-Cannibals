using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosterDissolve : MonoBehaviour
{
    public GameObject[] faces;

    public float ratePerSecond;

    private string shaderProperty = "Dissolve";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MakeDissolve()
    {
        foreach (GameObject face in faces)
        {
            Renderer rend = null;
            MaterialPropertyBlock propBlock = null;

            // Get renderer first time
            if (rend == null)
            {
                rend = GetComponent<Renderer>();
                propBlock = new MaterialPropertyBlock();
                rend.GetPropertyBlock(propBlock);
            }

            float prevValue = propBlock.GetFloat(shaderProperty);
            float newValue = prevValue += ratePerSecond;
            propBlock.SetFloat(shaderProperty, newValue);
            rend.SetPropertyBlock(propBlock);
        }
    }
  
}
