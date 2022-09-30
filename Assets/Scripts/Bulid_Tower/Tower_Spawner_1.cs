using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class Tower_Spawner_1 : MonoBehaviour
{
    public int count;
    public GameObject grey_prefab;
    public GameObject black_prefab;
    /*private void OnEnable()
    {
        for (int i = 0; i < count; i++)
        {
            GameObject gameobject_1;
            GameObject gameobject_2;

            if (i % 2 == 0)
            {
                gameobject_1 = Instantiate(grey_prefab);
                gameobject_2 = Instantiate(grey_prefab);
            }
            else
            {
                gameobject_1 = Instantiate(black_prefab);
                gameobject_2 = Instantiate(black_prefab);
            }
            gameobject_1.transform.SetParent(this.transform);
            gameobject_2.transform.SetParent(this.transform);
            gameobject_1.transform.localPosition = new Vector3(i * 2, 0, 0);
            gameobject_2.transform.localPosition = new Vector3(i * 2 - 1f, 1, 0);
        }
        for (int i = 0; i < count - 1; i++)
        {
            GameObject gameobject_1;
            GameObject gameobject_2;
            if (i % 2 == 0)
            {
                gameobject_1 = Instantiate(grey_prefab);
                gameobject_2 = Instantiate(grey_prefab);
            }
            else
            {
                gameobject_1 = Instantiate(black_prefab);
                gameobject_2 = Instantiate(black_prefab);
            }
            gameobject_1.transform.SetParent(this.transform);
            gameobject_2.transform.SetParent(this.transform);
            gameobject_1.transform.localPosition = new Vector3(i * 2 + 1f, 0, 1);
            gameobject_2.transform.localPosition = new Vector3(i * 2 + 2f, 1, 1);
        }
    }*/
    private void OnEnable()
    {
        for (int i = 0; i < count; i++)
        {
            GameObject gameobject_1;
            /*GameObject gameobject_2;*/

            if (i % 2 == 0)
            {
                gameobject_1 = Instantiate(grey_prefab);
                /*gameobject_2 = Instantiate(black_prefab);*/
            }
            else
            {
                gameobject_1 = Instantiate(black_prefab);
                /*gameobject_2 = Instantiate(grey_prefab);*/
            }
            gameobject_1.transform.SetParent(this.transform);
            /*gameobject_2.transform.SetParent(this.transform);*/
            gameobject_1.transform.localPosition = new Vector3(i * 2, 0, 0);
            /*gameobject_2.transform.localPosition = new Vector3(i * 2 , 0, 1);*/
        }
    }
}