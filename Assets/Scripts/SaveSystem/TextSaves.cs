using System.Collections;
using System.Collections.Generic;
using RougeFW;
using Unity.Entities.UniversalDelegates;
using UnityEngine;

public class TextSaves : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            SaveSystem.instance.ReadData("CommonData");

            
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            
            SaveSystem.instance.SaveData("CommonData");
           
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            SaveItemCommon saveItemCommon = SaveSystem.instance.GetSaveItem("CommonData") as SaveItemCommon ;

            if (saveItemCommon != null)
            {
                Debug.Log("读取文件成功");
                foreach (var pair in saveItemCommon.common_data)
                {
                    Debug.Log($"Key:{pair.Key}+value:{pair.Value}");
                }
            }
            else
            {
                Debug.Log("null");
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SaveItemCommon saveItemCommon = SaveSystem.instance.GetSaveItem("CommonData") as SaveItemCommon ;

            if (saveItemCommon != null)
            {
                Debug.Log("读取文件成功 修改文件");
                saveItemCommon.common_data.Add("3","shutgun");
                saveItemCommon.common_data.Add("4","mp5");

            }else
            {
                Debug.Log("null");
            }
        }

    }
}
