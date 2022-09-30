using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RougeFW
{


    public class UIPanelEvent : MonoBehaviour
    {
        public UIPanel ui_panel;


        public virtual void OnUpdateUIPanel()
        {
            //((WeaponUnlockPanel)ui_panel).UpdateUI();
        }
        public virtual void OnHidePanel()
        {
            ui_panel.HideUIPanel();
        }


    }
}
