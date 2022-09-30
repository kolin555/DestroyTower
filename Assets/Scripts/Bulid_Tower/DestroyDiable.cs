using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DestroyDiable : MonoBehaviour
{
    private Transform trans;
    private void Awake()
    {
        trans = this.transform;
    }
    private void OnEnable()
    {
        var chlids=trans.GetComponentsInChildren<Transform>(true);
        foreach(Transform child in chlids)
        {
            if (child.gameObject.activeInHierarchy == false)
            {
                DestroyImmediate(child.gameObject);
            }
        }
       
    }
}
