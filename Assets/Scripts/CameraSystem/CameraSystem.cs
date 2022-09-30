using System.Collections;
using System.Collections.Generic;
using Unity.XR.PXR;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.Experimental.XR;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
using UnityEngine.UI;
using Cinemachine;

public class CameraSystem : MonoBehaviour
{
    public static CameraSystem instance;
    public CameraWhiteMask camera_mask;
    /*public CameraWhiteMask_Image camera_mask_image;*/
    public List<Camera> camera_list;
    private void Awake()
    {
        instance = this;
    }
    private void OnDestroy()
    {
        instance = null;
    }


    public Camera main_camera;
    public List<CameraItem> camera_items = new List<CameraItem>();


    public List<CinemachineImpulseSource> impulse_sources = new List<CinemachineImpulseSource>();

    public bool is_from_start_up = false;

    public void RegisterItem()
    {
        camera_items = new List<CameraItem>(GetComponentsInChildren<CameraItem>(true));
        impulse_sources = new List<CinemachineImpulseSource>(GetComponentsInChildren<CinemachineImpulseSource>(true));

    }

    public void RegistItem(CameraItem camera_item)
    {
        if (camera_items.Contains(camera_item) == false)
            camera_items.Add(camera_item);
    }


    public void Impulse(string impulse_name)
    {
        for (int i = 0; i < impulse_sources.Count; i++)
            if (impulse_sources[i].name.Equals(impulse_name) == true)
                impulse_sources[i].GenerateImpulse();
    }
}
