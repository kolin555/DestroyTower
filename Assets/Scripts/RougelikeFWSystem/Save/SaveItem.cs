using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;



namespace RougeFW
{

    public class SaveItem : MonoBehaviour
    {
        public string save_file_name = "";
        public string item_status = "";

        public virtual void ReadData()
        {
            string save_file = Application.persistentDataPath + "/Save/" + (save_file_name.Equals("") == false ? save_file_name : gameObject.name) + ".dat";
            Debug.Log(save_file);
            if (File.Exists(save_file) == false)
                OnCreateData();
            string json_string = File.ReadAllText(save_file, Encoding.UTF8);
            UtilitySystem.ArchiveDecryption(ref json_string);
            
            OnReadFile(json_string);

        }

        public virtual void OnCreateData()
        {
            
        }

        public virtual void OnReadFile( string json_string )
        {
            
        }

        public virtual void OnSaveData()
        {
            
        }


        public virtual void SaveJsonToFile(string json_string)
        {
            string save_file = Application.persistentDataPath + "/Save/" + (save_file_name.Equals("") == false ? save_file_name : gameObject.name) + ".dat";

            UtilitySystem.ArchiveEncryption(ref json_string);
            File.WriteAllText(save_file, json_string, Encoding.UTF8);
        }

    }
}
