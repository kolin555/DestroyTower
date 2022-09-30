using System.Collections;
using System.Collections.Generic;
using RougeFW;
using UnityEngine;
using UnityEngine.Events;

public class VRGunUIButton : VRUIButton
{

    public GunType left_gun_type;
    public GunType right_gun_type;
    public UnityEvent<string> OnLeftTriggerClick;
    public UnityEvent<string> OnRightTriggerClick;
    
    public override void OnVRTriggerDownLeft(object[] objs)
    {
        if (gameObject.activeInHierarchy == false)
            return;

        if (left_on == true) OnLeftTriggerClick.Invoke($"{left_gun_type}");
        
    }

    public override void OnVRTriggerDownRight(object[] objs)
    {
        if (gameObject.activeInHierarchy == false)
            return;

        if (right_on == true) OnRightTriggerClick.Invoke($"{right_gun_type}");
    }
}
