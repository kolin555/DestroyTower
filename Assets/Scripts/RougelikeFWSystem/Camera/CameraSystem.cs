using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RougeFW
{


    public partial class CameraSystem : MonoBehaviour
    {

        public static CameraSystem instance;

        void Awake()
        {
            instance = this;
            RegisterItem();
        }

        void OnDestroy()
        {
            instance = null;
        }

        public Camera main_camera;
        public List<CameraItem> camera_items = new List<CameraItem>();


        public List<CinemachineImpulseSource> impulse_sources = new List<CinemachineImpulseSource>();

        public bool is_from_start_up = false;

        public void RegisterItem()
        {
            camera_items = new List<CameraItem>( GetComponentsInChildren<CameraItem>(true) );
            impulse_sources = new List<CinemachineImpulseSource>(GetComponentsInChildren<CinemachineImpulseSource>(true));
            
        }

        public void RegistItem( CameraItem camera_item )
        {
            if (camera_items.Contains(camera_item) == false)
                camera_items.Add(camera_item);
        }


        public void Impulse( string impulse_name )
        {
            for (int i = 0; i < impulse_sources.Count; i++)
                if (impulse_sources[i].name.Equals(impulse_name) == true)
                    impulse_sources[i].GenerateImpulse();
        }


    }
}
