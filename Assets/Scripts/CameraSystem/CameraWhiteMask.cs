using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.PXR;
public class CameraWhiteMask : MonoBehaviour
{
    public PXR_ScreenFade screenFade;
    
    public void FadeIn(float duration = 2.0f)
    {
        screenFade.fadeTime = duration;
        //screenFade.FadeIn();
    }
    public void FadeOut(float duration = 2.0f)
    {
        screenFade.fadeTime = duration;
        //screenFade.FadeOut();
    }
}
