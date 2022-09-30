using RougeFW;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Bullet_Info_Panel : UIPanel
{
    public TextMeshProUGUI bullet_info_left;
    public TextMeshProUGUI bullet_info_right;
    private void Update()
    {
        UpdateBulletInfo();
    }
    private void UpdateBulletInfo()
    {
        bullet_info_left.text = "Left_gun_info:" + Environment.NewLine
            + "Gun:" + GunController.gunInstance.currentLeftGun.name + Environment.NewLine
            + "Bullet_Velocity:" + BulletController.bulletInstance.shootVelocity_Left + Environment.NewLine
            + "Bullet_Mass:" + BulletController.bulletInstance.bulletMass_Left;

        bullet_info_right.text= "Right_gun_info:" + Environment.NewLine
            + "Gun:" + GunController.gunInstance.currentRightGun.name + Environment.NewLine
            + "Bullet_Velocity:" + BulletController.bulletInstance.shootVelocity_Right + Environment.NewLine
            + "Bullet_Mass:" + BulletController.bulletInstance.bulletMass_Right;
    }

}
