using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;



namespace RougeFW
{


    public class SaveSystem : MonoBehaviour
    {
        public static SaveSystem instance;

        void Awake()
        {
            instance = this;

            RegistItems();
        }

        void OnDestroy()
        {
            instance = null;
        }

       public  GenericDictionary<string, SaveItem> save_items = new GenericDictionary<string, SaveItem>();


        public void RegistItems()
        {
            SaveItem[] items = GetComponentsInChildren<SaveItem>(true);

            for (int i = 0; i < items.Length; i++)
                save_items.Add(items[i].name, items[i]);
        }


        public SaveItem GetSaveItem(string save_name)
        {
            return save_items.ContainsKey(save_name) == true ? save_items[save_name] : null;
        }



        public void ReadData(string save_name)
        {
            if (save_items.ContainsKey(save_name) == true)
            {
                string fileDic = Application.persistentDataPath + "/Save/";

                Debug.Log(fileDic);

                if (Directory.Exists(fileDic) == false)
                    Directory.CreateDirectory(fileDic);

                save_items[save_name].ReadData();
            }
        }


        public void SaveData(string save_name)
        {
            if (save_items.ContainsKey(save_name) == true)
                save_items[save_name].OnSaveData();
        }



        public static string common_data = "common_data";
        public static string account_data = "account_data";
        public static string currency_data = "currency_data";
        public static string iteminfo_data = "iteminfo_data";
        public static string equipmentinfo_data = "equipmentinfo_data";
        public static string iap_data = "iap_data";
        public static string role_data = "role_data";
        public static string talent_data = "talent_data";
        public static string achievement_data = "achievement_data";
    }
}
