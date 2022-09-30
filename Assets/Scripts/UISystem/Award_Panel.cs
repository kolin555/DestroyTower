using System;
using System.Collections;
using System.Collections.Generic;
using RougeFW;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using TMPro;
using DG.Tweening;

public class Award_Panel : UIPanel
{
    

    /*public Button gunType;
    public Button bulletType;
    public Button bulletNumber;

    public bool is_trigger_down_left;
    public bool is_trigger_Down_right;
    
    
    
    
    
    public override void Init()
    {
        UIManager.ui_Instance.ShowPanel<Award_Panel>();
        
        //右手的
        gunType.onClick.AddListener(() =>
        {
            /*GunController.gunInstance.AddGunToLeftList(GunType.shotgun1_Left);
            GunController.gunInstance.AddGunToRightList(GunType.shotgun1_Right);#1#
            
         
            UIManager.ui_Instance.HidePanel<Award_Panel>();
            /*Time.timeScale = 1;#1#
        });
        bulletType.onClick.AddListener(() =>
        {

            if (is_trigger_down_left)
            {
                BulletController.bulletInstance.AddBulletTypeToRightList("Bullet_4");
                
            }
            if (is_trigger_Down_right)
            {
                BulletController.bulletInstance.AddBulletTypeToLeftList("Bullet_4");
                Debug.Log("addRightGun");
            }

           
            /*Time.timeScale = 1;#1#
            UIManager.ui_Instance.HidePanel<Award_Panel>();
        });
        bulletNumber.onClick.AddListener(() =>
        {

            if (is_trigger_down_left)
            {
                BulletController.bulletInstance.AddBulletNumberToRightList("Bullet_Spawn_Nine");

            }
            if (is_trigger_Down_right)
            {
                BulletController.bulletInstance.AddBulletNumberToLeftList("Bullet_Spawn_Nine");
               
            }
           /* BulletController.bulletInstance.AddBulletNumberToRightList("Bullet_Spawn_Nine");
            BulletController.bulletInstance.AddBulletNumberToLeftList("Bullet_Spawn_Nine");#1#
      
            //添加子弹的数目
            
            /*Time.timeScale = 1;#1#
            UIManager.ui_Instance.HidePanel<Award_Panel>();
        });
        

    }
    
    
    public void Left_trigger_Down (object[] objs)
    {
        is_trigger_down_left = true;
    }
    public void Right_trigger_Down(object[] objs)
    {
        is_trigger_Down_right = true;
    }
    public void Left_trigger_up(object[] objs)
    {
        is_trigger_down_left = false;
    }
    public void Right_trigger_up(object[] objs)
    {
        is_trigger_Down_right = false;
    }*/


   /* public VRGunUIButton gunTypeButton;
    public VRBulletTypeUIButton bulletTypeUiButton;
    public VRBulletNumUIButton bulletNumUiButton;
    public VRUIButton nextLevelButton;*/
    public VRBulletMassUpUIButton bulletMassUpUIButton;
    public VRBulletNumUpUIButton bulletNumUpUIButton;
    public VRBulletVelocityUpUIButton bulletVelocityUpUIButton;
    public TextMeshProUGUI level_id;



    private void Start()
    {
        /*gunTypeButton.OnLeftTriggerClick.AddListener(GunController.gunInstance.AddGunToLeftList);
        gunTypeButton.OnRightTriggerClick.AddListener(GunController.gunInstance.AddGunToRightList);

        bulletTypeUiButton.OnLeftTriggerClick.AddListener(BulletController.bulletInstance.AddBulletTypeToLeftList);
        bulletTypeUiButton.OnRightTriggerClick.AddListener(BulletController.bulletInstance.AddBulletTypeToRightList);

        bulletNumUiButton.OnLeftTriggerClick.AddListener(BulletController.bulletInstance.AddBulletNumberToLeftList);
        bulletNumUiButton.OnRightTriggerClick.AddListener(BulletController.bulletInstance.AddBulletNumberToRightList);

        nextLevelButton.OnClick.AddListener(NextLevel);
*/
        bulletMassUpUIButton.OnLeftTriggerClick.AddListener(NextLevel);
        bulletMassUpUIButton.OnRightTriggerClick.AddListener(NextLevel);
        bulletMassUpUIButton.OnLeftTriggerClick.AddListener(BulletController.bulletInstance.MassUp_Left);
        bulletMassUpUIButton.OnRightTriggerClick.AddListener(BulletController.bulletInstance.MassUp_Right);

        bulletNumUpUIButton.OnLeftTriggerClick.AddListener(NextLevel);
        bulletNumUpUIButton.OnRightTriggerClick.AddListener(NextLevel);
        bulletNumUpUIButton.OnLeftTriggerClick.AddListener(BulletController.bulletInstance.ChangeLeftBulletPoint);
        bulletNumUpUIButton.OnRightTriggerClick.AddListener(BulletController.bulletInstance.ChangeRightBulletPoint);


        bulletVelocityUpUIButton.OnLeftTriggerClick.AddListener(NextLevel);
        bulletVelocityUpUIButton.OnRightTriggerClick.AddListener(NextLevel);
        bulletVelocityUpUIButton.OnLeftTriggerClick.AddListener(BulletController.bulletInstance.VelocityUp_Left);
        bulletVelocityUpUIButton.OnRightTriggerClick.AddListener(BulletController.bulletInstance.VelocityUp_Right);

        /*  bulletMassUpUIButton.OnClick.AddListener(NextLevel);
          bulletNumUpUIButton.OnClick.AddListener(NextLevel);
          bulletVelocityUpUIButton.OnClick.AddListener(NextLevel);*/
       
    }
    private void OnEnable()
    {
        /*level_id.text = (LevelContoller.levelInstance.tower_Id).ToString();*/
        HideVRButton();
    }
    public override void ShowUIPanel()
    {
        base.ShowUIPanel();
        level_id.text = (LevelContoller.levelInstance.tower_Id).ToString();
        ShowVRButton();
    }
    public void ShowVRButton()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.SetDelay(2f);
        sequence.AppendCallback(() =>
        {
            bulletMassUpUIButton.gameObject.SetActive(true);
            bulletNumUpUIButton.gameObject.SetActive(true);
            bulletVelocityUpUIButton.gameObject.SetActive(true);
        });

    }
    public void HideVRButton()
    {
        bulletMassUpUIButton.gameObject.SetActive(false);
        bulletNumUpUIButton.gameObject.SetActive(false);
        bulletVelocityUpUIButton.gameObject.SetActive(false);
    }
    public void NextLevel()
    {
        /*Time.timeScale = 1;*/
        
        UISystem.instance.HideUIPanel("Award_Panel");
        LevelContoller.levelInstance.AfterALevel();

    }
    
 


}

