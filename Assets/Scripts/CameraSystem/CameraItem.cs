using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class CameraItem : MonoBehaviour
    {
        public bool is_active_when_start = false;


        public Cinemachine.CinemachineVirtualCamera virtual_camera;


        private void Start()
        {
            CameraSystem.instance.RegistItem(this);
            virtual_camera.gameObject.SetActive(is_active_when_start);
        }

    }

