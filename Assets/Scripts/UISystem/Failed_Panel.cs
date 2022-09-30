using System.Collections;
using System.Collections.Generic;
using RougeFW;
using UnityEngine;
using UnityEngine.UI;

public class Failed_Panel : UIPanel
{

    public VRUIButton restart_button;
    public VRUIButton exit_button;

    private void Awake()
    {
        restart_button.OnClick.AddListener(ReStartAction);
        exit_button.OnClick.AddListener(ExitAction);
    }
    public void ReStartAction()
    {
        UISystem.instance.HideUIPanel("Failed_Panel");
        LevelContoller.levelInstance.tower_Id = 0;
        LevelContoller.levelInstance.AfterALevel();
        

    }
    public void ExitAction()
    {
        Application.Quit();
    }

}
