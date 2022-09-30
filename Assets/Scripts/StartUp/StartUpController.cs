using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using RougeFW;
using UnityEngine;
using UnityEngine.UI;
using Unity.XR.PXR;

public class StartUpController : MonoBehaviour
{
    /*public GameObject start_Panel;*/

    private PXR_OverLay overLay;

    public bool is_press_key = false;
    public bool is_fade_out = false;
    private void Awake()
    {
        
    }
    private void Start()
    {
        CameraSystem.instance.is_from_start_up = true;
        MsgSystem.instance.RegistMsgAction(MsgSystem.vr_button_a_down, OnPressKey);
        MsgSystem.instance.RegistMsgAction(MsgSystem.vr_button_b_down, OnPressKey);
        MsgSystem.instance.RegistMsgAction(MsgSystem.vr_button_x_down, OnPressKey);
        MsgSystem.instance.RegistMsgAction(MsgSystem.vr_button_y_down, OnPressKey);

        MsgSystem.instance.RegistMsgAction(MsgSystem.vr_trigger_down_left, OnPressKey);
        MsgSystem.instance.RegistMsgAction(MsgSystem.vr_trigger_down_right, OnPressKey);

        MsgSystem.instance.RegistMsgAction(MsgSystem.vr_hold_down_left, OnPressKey);
        MsgSystem.instance.RegistMsgAction(MsgSystem.vr_hold_down_right, OnPressKey);
/*
        MsgSystem.instance.RegistMsgAction(MsgSystem.battle_ready, OnBattleReady);*/


        /*CameraSystem.instance.camera_mask.screenFade.SetAlpha(1);*/
        //CameraSystem.instance.camera_mask.FadeIn(0.01f);

        StartCoroutine(LoadBattle());
        
        Sequence q = DOTween.Sequence();
            q.SetDelay(3);
            q.AppendCallback(() =>
            {
                if (is_fade_out == false)
                {

                    CameraSystem.instance.camera_mask.FadeIn();
                    //UIManager.ui_Instance.ShowPanel<Main_Panel>();
                    /*CameraSystem.instance.camera_mask_image.FadeIn();*/
                    UISystem.instance.ShowUIPanel("Main_Panel");
                    is_fade_out = true;
                }
               /* if (start_Panel != null)
                {
                    Destroy(start_Panel);
                }*/
            });
        
    }
    private void ShowLogo()
    {
        
    }
    private void Update()
    {
    }
    public void  OnPressKey(System.Object[] objs)
    {
        is_press_key = true;
    }
   /* public void OnBattleReady(System.Object[] objs)
    {
        if (start_Panel != null)
        {
            Destroy(start_Panel);

        }
    }*/
    IEnumerator LoadBattle()
    {
        while (true)
        {
            if (is_fade_out == true)
            {
                yield break;
            }
            if (is_press_key)
            {
                CameraSystem.instance.camera_mask.FadeIn();
                //UIManager.ui_Instance.ShowPanel<Main_Panel>();

                /*CameraSystem.instance.camera_mask_image.FadeIn();*/
                UISystem.instance.ShowUIPanel("Main_Panel");
                is_fade_out = true;
                yield break;
            }
          
            
            yield return null;
        }

    }
}
