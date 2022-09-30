using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Bulid_Tower_1 : MonoBehaviour
{
    public int width;
    public int height;
    public float wid_space;
    public float height_space;
    public GameObject grey_00;
    /*public GameObject black_01;*/
    private void OnEnable()
    {
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
               GameObject gameobject = Instantiate(grey_00);
                gameobject.transform.SetParent(this.transform);
                gameobject.transform.localPosition = new Vector3(i * wid_space, 0, j*height_space);
            }
           
        }
    }
}
