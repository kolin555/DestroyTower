using System.Collections;
using System.Collections.Generic;
using RougeFW;
using UnityEngine;
using UnityEngine.UI;

public class Main_Panel :UIPanel
{
    /*
    public Button start_Button;
    public Button scene_Button;
    public Button exit_Button;
    public override void Init()
    {
        /*Time.timeScale = 0;#1#
        start_Button.onClick.AddListener(() =>
        {
            LevelContoller.levelInstance.InitALevel(1);
            UIManager.ui_Instance.HidePanel<Main_Panel>();
        });
        scene_Button.onClick.AddListener(() =>
        {

        });
        exit_Button.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
    */
    public VRUIButton start_button;
    private void Awake()
    {
        start_button.OnClick.AddListener(StartButtonAction);
    }
    public void StartButtonAction()
    {
        LevelContoller.levelInstance.InitALevel(LevelContoller.levelInstance.tower_Id);
        //UIManager.ui_Instance.HidePanel<Main_Panel>();
        UISystem.instance.HideUIPanel("Main_Panel");
        UISystem.instance.ShowUIPanel("Process_Panel");
        MsgSystem.instance.SendMsg("battle_ready", new object[] { "level1" });
    }
    
    
}
