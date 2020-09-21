using UnityEngine;
using System.Collections;

namespace SceneObjects
{
    public class StoveIndicatorLight : MonoBehaviour
    {
        public Color onColor = Color.green;
        public Color offColor = Color.red;

        private Renderer rend;
        private MaterialPropertyBlock propBlock;
        private readonly string colorShaderProp = "Color_72E286ED";

        void Start()
        {
            rend = GetComponent<Renderer>();
            propBlock = new MaterialPropertyBlock();
            rend.GetPropertyBlock(propBlock);
        }

        public void TurnOn()
        {
            propBlock.SetColor(colorShaderProp, onColor);
            rend.SetPropertyBlock(propBlock);
        }

        public void TurnOff()
        {
            propBlock.SetColor(colorShaderProp, offColor);
            rend.SetPropertyBlock(propBlock);
        }
    }
}