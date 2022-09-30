using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcessFilled_Image : MonoBehaviour
{
    public Image filled_Image;
    private void Awake()
    {
        filled_Image = this.gameObject.GetComponent<Image>();

    }
    private void Update()
    {
        filled_Image.fillAmount = 1-(float)(LevelContoller.levelInstance.current_DestroyBoxCount*10000 / LevelContoller.levelInstance.target_BoxCount)/10000;
    }
}
