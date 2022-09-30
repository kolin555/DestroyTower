using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Process_Slider : MonoBehaviour
{
    
    public  RectTransform filled_trans;
    public RectTransform frame_trans;
    private Slider process_slider;
    private TextMeshProUGUI process_text;
    private void Awake()
    {
        process_slider = this.transform.GetComponent<Slider>();
        process_text = this.transform.GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Update()
    {

        if (LevelContoller.levelInstance.isInit == true)
        {
            process_slider.value = 0;

        }
        else
        {
            process_slider.value =
                (float)(LevelContoller.levelInstance.current_DestroyBoxCount * 1000
                / LevelContoller.levelInstance.target_BoxCount) / 1000;
            
        }
        process_text.text = (int)(process_slider.value*100) + " %";
        filled_trans.anchoredPosition=
            new Vector2( process_slider.value * frame_trans.rect.width,
            filled_trans.anchoredPosition.y);
        
    }
}
