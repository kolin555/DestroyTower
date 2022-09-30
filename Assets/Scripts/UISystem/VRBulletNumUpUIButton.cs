using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VRBulletNumUpUIButton : VRUIButton
{
    public UnityEvent OnLeftTriggerClick;
    public UnityEvent OnRightTriggerClick;

    public override void OnVRTriggerDownLeft(object[] objs)
    {
        if (gameObject.activeInHierarchy == false)
            return;

        if (left_on == true)
        {
            //����ӵ���
            /*BulletController.bulletInstance.ChangeLeftBulletPoint();*/
            OnLeftTriggerClick.Invoke();
        }
    }

    public override void OnVRTriggerDownRight(object[] objs)
    {
        if (gameObject.activeInHierarchy == false)
            return;

        if (right_on == true)
        {
            //����ӵ���
            /*BulletController.bulletInstance.ChangeLeftBulletPoint();*/
            OnRightTriggerClick.Invoke();
        }
    }
}
