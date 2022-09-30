using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RougeFW;
using TMPro;
using UnityEngine.UI;

public class Process_Panel : UIPanel
{
    /*[SerializeField]
    private TextMeshProUGUI timeDown_text;*/

    [SerializeField]
    private TextMeshProUGUI towerId_text;

    [SerializeField]
    private TextMeshProUGUI bulletCount_text;

    [SerializeField]
    private TextMeshProUGUI boxCount_text;

    [SerializeField]
    private Image filled_Image;

    [SerializeField]
    private TextMeshProUGUI filled_text;

 
    private void Start()
    {

        /*fontcolor = timeDown_text.color;*/
    }
    private void Update()
    {
        
        UpdateTowerIdInfo();
        UpdateBulletInfo();
     
        UpdateProcessInfo();
    }
  
    private void UpdateTowerIdInfo()
    {
        towerId_text.text ="LEVEL:" + LevelContoller.levelInstance.tower_Id.ToString();
    }
 
    private void UpdateBoxCount()
    {
        boxCount_text.text ="BOXCOUNT:"+ LevelContoller.levelInstance.total_BoxCount.ToString();
    }
    private void UpdateBulletInfo()
    {
        var currentShootCount = (BulletController.bulletInstance.currentShootCount >= 0) ?
                             BulletController.bulletInstance.currentShootCount : 0;
        bulletCount_text.text ="BULLET:"+ 
            currentShootCount.ToString()+"/"+
            BulletController.bulletInstance.maxShootCount;
    }
    private void UpdateProcessInfo()
    {
        if (LevelContoller.levelInstance.isInit == true)
        {
            filled_Image.fillAmount = 0;

        }
        else
        {
            filled_Image.fillAmount =
                (float)(LevelContoller.levelInstance.current_DestroyBoxCount * 1000
                / LevelContoller.levelInstance.target_BoxCount) / 1000;

        }
        filled_text.text = (int)(filled_Image.fillAmount * 100) + " %";
       

    }


}
