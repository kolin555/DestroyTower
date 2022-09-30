using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VRBulletVelocityUpUIButton : VRUIButton
{
    public UnityEvent OnLeftTriggerClick;
    public UnityEvent OnRightTriggerClick;

    public override void OnVRTriggerDownLeft(object[] objs)
    {
        if (gameObject.activeInHierarchy == false)
            return;

        if (left_on == true)
        {
            /*var shootVelocity = BulletController.bulletInstance.shootVelocity_Left;
            var shootVelocity_Max = BulletController.bulletInstance.shootVelocity_Max;
            shootVelocity = Mathf.Clamp(shootVelocity+100, 0, shootVelocity_Max);
            BulletController.bulletInstance.shootVelocity_Left = shootVelocity;*/
            OnLeftTriggerClick.Invoke();
        }
    }

    public override void OnVRTriggerDownRight(object[] objs)
    {
        if (gameObject.activeInHierarchy == false)
            return;

        if (right_on == true)
        {
           /* var shootVelocity = BulletController.bulletInstance.shootVelocity_Right;
            var shootVelocity_Max = BulletController.bulletInstance.shootVelocity_Max;
            shootVelocity = Mathf.Clamp(shootVelocity+100, 0, shootVelocity_Max);
            BulletController.bulletInstance.shootVelocity_Right = shootVelocity;*/
            OnRightTriggerClick.Invoke();
        }
    }
}
