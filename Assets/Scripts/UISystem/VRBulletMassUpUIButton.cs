using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VRBulletMassUpUIButton : VRUIButton
{
    public UnityEvent OnLeftTriggerClick;
    public UnityEvent OnRightTriggerClick;
    public override void OnVRTriggerDownLeft(object[] objs)
    {
        if (gameObject.activeInHierarchy == false)
            return;

        if (left_on == true)
        {
            /*var bulletMass = BulletController.bulletInstance.bulletMass_Left;
            var bulletMass_Max = BulletController.bulletInstance.bulletMass_Max;
            bulletMass = Mathf.Clamp(bulletMass + 5, 0, bulletMass_Max);
            BulletController.bulletInstance.bulletMass_Left = bulletMass;*/
            OnLeftTriggerClick.Invoke();
        }
    }

    public override void OnVRTriggerDownRight(object[] objs)
    {
        if (gameObject.activeInHierarchy == false)
            return;

        if (right_on == true)
        {
           /* var bulletMass = BulletController.bulletInstance.bulletMass_Right;
            var bulletMass_Max = BulletController.bulletInstance.bulletMass_Max;
            bulletMass = Mathf.Clamp(bulletMass + 5, 0, bulletMass_Max);
            BulletController.bulletInstance.bulletMass_Right = bulletMass;*/
            OnRightTriggerClick.Invoke();
        }
    }
}
