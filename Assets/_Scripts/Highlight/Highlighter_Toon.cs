using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using Cooking;

namespace Highlight
{
    public class Highlighter_Toon : VRTK_InteractObjectHighlighter
    {
        private string shaderProperty = "_isHighlighted";

        private readonly float ON = 1;
        private readonly float OFF = 0;

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

            propBlock.SetFloat(shaderProperty, ON);
            rend.SetPropertyBlock(propBlock);
            isHighlighted = true;
        }

        private void RemoveTouchHighlight()
        {
            if (isHighlighted)
            {
                propBlock.SetFloat(shaderProperty, OFF);
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