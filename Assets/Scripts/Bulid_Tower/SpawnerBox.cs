using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SpawnerBox : MonoBehaviour
{
    public int layerCount;
    public int widthCount;
    public GameObject boxPrefab;
    

    private void Update()
    {
        var count = this.transform.childCount;
        if (count!= 0)
        {
            for(int i = 0; i < count; i++)
            {
                GameObject obj = this.transform.GetChild(0).gameObject;
                
                DestroyImmediate(obj);
            }
        }
        for (var y = 0; y < layerCount; y++)
        {
            for (var x = 0; x < widthCount; x++)
            {
                for (var z = 0; z < widthCount; z++)
                {

                    var instance = Instantiate(boxPrefab);

                    instance.transform.SetParent(this.transform, false);
                    if (widthCount % 2 == 0)
                    {
                        instance.transform.localPosition = new Vector3(x - widthCount / 2 + 0.5f, y, z - widthCount / 2 + 0.5f);

                    }
                    else
                    {
                        instance.transform.localPosition = new Vector3(x - widthCount / 2, y, z - widthCount / 2);
                    }



                }
            }
        }
    }

}
