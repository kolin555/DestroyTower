using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Newtonsoft.Json;

namespace RougeFW
{


    public class SaveItemCommon : SaveItem
    {
        public Dictionary<string, string> common_data = new Dictionary<string, string>();

        public override void OnCreateData()
        {
            common_data = new Dictionary<string, string>();

            item_status = "create";

            SaveJsonToFile(JsonConvert.SerializeObject(common_data));
        }

        public override void OnReadFile(string json_string)
        {
            item_status = "read";

            common_data = JsonConvert.DeserializeObject<Dictionary<string, string>>(json_string);
        }


        public override void OnSaveData()
        {
            SaveJsonToFile(JsonConvert.SerializeObject(common_data));
        }



    }
}
