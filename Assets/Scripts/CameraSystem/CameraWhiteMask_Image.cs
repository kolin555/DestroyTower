using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class CameraWhiteMask_Image : MonoBehaviour
{
    public Image  fade_Image;

    public void FadeIn(float duration = 2.0f)
    {
        fade_Image.DOFade(0, duration);
    }
    public void FadeOut(float duration = 2.0f)
    {
        fade_Image.DOFade(1, duration);
    }
    public void SetColor(Color color)
    {
        fade_Image.color = color;
    }
   
}
