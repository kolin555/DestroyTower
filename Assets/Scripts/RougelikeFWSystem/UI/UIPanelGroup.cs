using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace RougeFW
{



    public class UIPanelGroup : MonoBehaviour
    {
        public List<string> panel_names = new List<string>();


        public void OnClickShowPanel(string panel_name)
        {
            for (int i = 0; i < panel_names.Count; i++)
                if (panel_names[i].Equals(panel_name) == true)
                    UISystem.instance.ShowUIPanel(panel_names[i]);
                else
                    UISystem.instance.HideUIPanel(panel_names[i]);
        }


    }

}
