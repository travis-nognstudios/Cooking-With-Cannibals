using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using Cooking;

namespace Highlight
{
    public class Highlighter_Toon : VRTK_InteractObjectHighlighter
    {

        [Header("Shader Variables")]
        public string shaderProperty = "Color_72E286ED";
        
        private Color baseColor = new Color(1, 1, 1);

        private Renderer rend;
        private Material mat;
        private MaterialPropertyBlock propBlock;
        private bool isHighlighted;

        private Cookablev2 cookable;

        public override void Highlight(Color highlightColor)
        {
            SetTouchHighlight();
            base.Highlight(highlightColor);
            SetMaterialCookable();
        }

        public override void Unhighlight()
        {
            RemoveTouchHighlight();
            base.Unhighlight();
            SetMaterialCookable();
        }

        private void SetTouchHighlight()
        {
            // Get renderer first time
            if (rend == null)
            {
                rend = GetComponent<Renderer>();
                propBlock = new MaterialPropertyBlock();
                rend.GetPropertyBlock(propBlock);

                cookable = GetComponent<Cookablev2>();
            }

            propBlock.SetColor(shaderProperty, touchHighlight);
            rend.SetPropertyBlock(propBlock);
            isHighlighted = true;
        }

        private void RemoveTouchHighlight()
        {
            if (isHighlighted)
            {
                propBlock.SetColor(shaderProperty, baseColor);
                rend.SetPropertyBlock(propBlock);
                isHighlighted = false;
            }
        }

        // Cookable material must be reset after base highlighter calls
        private void SetMaterialCookable()
        {
            if (cookable != null)
            {
                mat = cookable.GetCurrentStateMat();
                rend.material = mat;
            }
        }
    }
}