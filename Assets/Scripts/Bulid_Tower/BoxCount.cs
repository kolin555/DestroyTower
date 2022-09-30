using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class BoxCount : MonoBehaviour
{
    private Transform trans;
    public int active_count;
    public int total_count;
    private void Awake()
    {
        trans = this.transform;
        
    }
    private void OnEnable()
    {
        var chlidren=trans.GetComponentsInChildren<Transform>();
        active_count = chlidren.Length;
        var children_2 = trans.GetComponentsInChildren<Transform>(true);
        total_count = children_2.Length;
    }
}
