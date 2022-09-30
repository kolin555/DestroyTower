using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace RougeFW
{


    public class UISystem : MonoBehaviour
    {
        public static UISystem instance;

        

        void Awake()
        {
            instance = this;

            RegistItems();
        }

        void OnDestroy()
        {
            instance = null;
        }

        public List<UIPanel> ui_panels = new List<UIPanel>();

        public void RegistItems()
        {
            ui_panels = new List<UIPanel>( GetComponentsInChildren<UIPanel>(true) );
        }



        public void RegistUIPanel( UIPanel panel )
        {
            if ( ui_panels.Contains(panel) == false )
                ui_panels.Add( panel);

            ui_panels.Sort((x, y) => { return -x.priority.CompareTo(y.priority); });
        }


        public void RemoveUIPanel( UIPanel panel)
        {
            if ( ui_panels.Contains(panel) == true )
                ui_panels.Remove( panel);
        }



        public UIPanel GetUIPanel(string panel_name)
        {
            return ui_panels.Find( x=>x.name.Equals(panel_name) );
        }


       
        void Start()
        {
            ui_panels.Sort((x, y) => { return -x.priority.CompareTo(y.priority); });

            for (int i = 0; i < ui_panels.Count; i++)
                ui_panels[i].StartPanel();
        }



        public void ShowUIPanel( string panel_name )
        {
            UIPanel panel = GetUIPanel(panel_name);

            Debug.Log(panel.name);

            if (panel != null)
                panel.ShowUIPanel();


        }

        public void HideUIPanel(string panel_name)
        {
            UIPanel panel = GetUIPanel(panel_name);

            if (panel != null)
                panel.HideUIPanel();

        }

    }
}
