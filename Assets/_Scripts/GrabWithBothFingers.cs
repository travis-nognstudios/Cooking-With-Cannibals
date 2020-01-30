using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class GrabWithBothFingers : VRTK_ControllerEvents
{
    // Keep track of whether grip is pressed because
    // of real grip press or alias = trigger press
    private bool triggerIsGrabbing;
    
    public override void OnTriggerPressed(ControllerInteractionEventArgs e)
    {
        if (!gripPressed)
        {
            base.OnGripPressed(e);
        }

    }
    
    public override void OnTriggerReleased(ControllerInteractionEventArgs e)
    {
        OnGripReleased(e);
    }

    public override void OnGripReleased(ControllerInteractionEventArgs e)
    {
        if (!gripPressed && !triggerPressed)
        {
            base.OnGripReleased(e);
        }
    }

}
