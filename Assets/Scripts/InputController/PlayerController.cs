using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using RougeFW;
public class PlayerController : MonoBehaviour
{
        public static PlayerController instance;
   
       /* private EntityManager entityManager;*/

        public  XRRayInteractor xr_L;
        public  XRRayInteractor xr_R;


        void Awake()
        {
            instance = this;
            /*entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;*/
            xr_L = GameObject.FindGameObjectWithTag("LController").GetComponent<XRRayInteractor>();
            xr_R = GameObject.FindGameObjectWithTag("RController").GetComponent<XRRayInteractor>();
        }
      
       
        void OnDestroy()
        {
            instance = null;
            
        }

  
   void Start()
   {

        // 监听扳机键
        MsgSystem.instance.RegistMsgAction(MsgSystem.vr_trigger_down_left, OnVRTriggerDownLeft);
        MsgSystem.instance.RegistMsgAction(MsgSystem.vr_trigger_up_left, OnVRTriggerUpLeft);
        MsgSystem.instance.RegistMsgAction(MsgSystem.vr_trigger_down_right, OnVRTriggerDownRight);
        MsgSystem.instance.RegistMsgAction(MsgSystem.vr_trigger_up_right, OnVRTriggerUpRight);
        // 监听握把键
        MsgSystem.instance.RegistMsgAction(MsgSystem.vr_hold_down_left, OnVRHoldDownLeft);
        MsgSystem.instance.RegistMsgAction(MsgSystem.vr_hold_up_left, OnVRHoldUpLeft);
        MsgSystem.instance.RegistMsgAction(MsgSystem.vr_hold_down_right, OnVRHoldDownRight);
        MsgSystem.instance.RegistMsgAction(MsgSystem.vr_hold_up_right, OnVRHoldUpRight);

        //监听右手AB键
        MsgSystem.instance.RegistMsgAction(MsgSystem.vr_button_a_down, OnVRButtonADown);
        MsgSystem.instance.RegistMsgAction(MsgSystem.vr_button_a_up, OnVRButtonAUp);

        MsgSystem.instance.RegistMsgAction(MsgSystem.vr_button_b_down, OnVRButtonBDown);
        MsgSystem.instance.RegistMsgAction(MsgSystem.vr_button_b_up, OnVRButtonBUp);

        MsgSystem.instance.RegistMsgAction(MsgSystem.vr_button_x_down, OnVRButtonXDown);
        MsgSystem.instance.RegistMsgAction(MsgSystem.vr_button_x_up, OnVRButtonXUp);

        MsgSystem.instance.RegistMsgAction(MsgSystem.vr_button_y_down, OnVRButtonYDown);
        MsgSystem.instance.RegistMsgAction(MsgSystem.vr_button_y_up, OnVRButtonYUp);
    }

    private void OnVRButtonYUp(System.Object[] objs)
    {
        
    }

    private void OnVRButtonYDown(System.Object[] objs)
    {
        //左手子弹类型切换
        /*BulletController.bulletInstance.ChangeLeftBulletType();*/
        BulletController.bulletInstance.MassUp_Left();
   
    }

    private void OnVRButtonXUp(System.Object[] objs)
    {
        
    }

    private void OnVRButtonXDown(System.Object[] objs)
    {
        //左手子弹个数切换
        /*BulletController.bulletInstance.ChangeLeftBulletPoint();*/
        BulletController.bulletInstance.VelocityUp_Left();
    }

    private void OnVRButtonBUp(System.Object[] objs)
    {
        
    }

    private void OnVRButtonBDown(System.Object[] objs)
    {
        //右手子弹类型切换
        /*BulletController.bulletInstance.ChangeRightBulletType();*/
        BulletController.bulletInstance.MassUp_Right();

    }

    private void OnVRButtonAUp(System.Object[] objs)
    {
        
    }

    private void OnVRButtonADown(System.Object[] objs)
    {
        //右手子弹个数切换
        /*BulletController.bulletInstance.ChangeRightBulletPoint();*/
        BulletController.bulletInstance.VelocityUp_Right();
    }

    private void Update()
    {
        
       /* if (LevelContoller.levelInstance.time_Down >= 60f)
        {
            xr_R.enabled = true;
            xr_L.enabled = true;
        }
        else
        {
            xr_R.enabled = false;
            xr_L.enabled = false;
        }*/
    }

   

    private void OnVRHoldUpRight(System.Object[] objs)
    {
        
    }

    private void OnVRHoldDownRight(System.Object[] objs)
    {
        //右手换枪
        GunController.gunInstance.ChangeRightGun();
    }

    private void OnVRHoldUpLeft(System.Object[] objs)
    {
        
    }

    private void OnVRHoldDownLeft(System.Object[] objs)
    {
        //左手换枪
        GunController.gunInstance.ChangeLeftGun();
    }

    // 扳机
    public void OnVRTriggerDownLeft(System.Object[] objs)
    {
        //左手射击
        
        BulletController.bulletInstance.Spawn_Left();

    }
    public void OnVRTriggerUpLeft(System.Object[] objs)
    {
            
    }

    public void OnVRTriggerDownRight(System.Object[] objs)
    {
        //右手射击
        /*   if (Time.timeScale == 0)
           {
               return;
           }*/
        
        BulletController.bulletInstance.Spawn_Right();
    }

    public void OnVRTriggerUpRight(System.Object[] objs)
    {
            
    }


}


