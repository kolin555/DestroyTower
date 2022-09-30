using System.Collections;
using System.Collections.Generic;
using RougeFW;
using UnityEngine;

public class Pause_Panel : UIPanel
{
    public void ReStartAction()
    {
        UISystem.instance.HideUIPanel("Pause_Panel");
        Time.timeScale = 1;

    }
    public void ExitAction()
    {
        Application.Quit();
    }
}
