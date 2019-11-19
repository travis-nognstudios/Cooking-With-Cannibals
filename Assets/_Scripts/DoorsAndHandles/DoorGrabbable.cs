using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

namespace DoorsAndHandles
{
    public class DoorGrabbable : VRTK.VRTK_InteractableObject
    {
        public Transform doorHandler;

        public override void OnInteractableObjectUngrabbed(InteractableObjectEventArgs e)
        {
            base.OnInteractableObjectUngrabbed(e);
            this.transform.position = doorHandler.transform.position;
            this.transform.rotation = doorHandler.transform.rotation;
        }
    }
}