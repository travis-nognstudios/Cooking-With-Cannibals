using System;
using UnityEngine;

namespace Serving
{
    public abstract class RecipeSequence : MonoBehaviour
    {
        public abstract Recipe[] GetRecipes();
    }
}