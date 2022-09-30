using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class changePosition : MonoBehaviour
{
    private Transform trans;
    public float padding_x;
    public float padding_y;
    public float padding_z;
    private void Awake()
    {
        trans = this.transform;

    }
    private void OnEnable()
    {
        foreach(Transform child in trans)
        {
            var pos = child.position;
            child.position = new Vector3(pos.x + padding_x, pos.y + padding_y, pos.z + padding_z);
        }
    }
}
