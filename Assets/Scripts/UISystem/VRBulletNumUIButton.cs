using System.Collections;
using System.Collections.Generic;
using RougeFW;
using UnityEngine;
using UnityEngine.Events;

public class VRBulletNumUIButton : VRUIButton
{
    public BulletNumType left_bullet_num_Type;
    public BulletNumType right_bullet_num_Type;
    
    public UnityEvent<string> OnLeftTriggerClick;
    public UnityEvent<string> OnRightTriggerClick;
    
    public override void OnVRTriggerDownLeft(object[] objs)
    {
        if (gameObject.activeInHierarchy == false)
            return;

        if (left_on == true) OnLeftTriggerClick.Invoke($"{left_bullet_num_Type}");
    }

    public override void OnVRTriggerDownRight(object[] objs)
    {
        if (gameObject.activeInHierarchy == false)
            return;

        if (right_on == true) OnRightTriggerClick.Invoke($"{right_bullet_num_Type}");
    }
}