using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class OnlyOneInPosition : MonoBehaviour
{
    private Transform trans;
   
   
    private void Awake()
    {
        trans = this.transform;
    }
    private void OnEnable()
    {

     
        var chlids = trans.GetComponentsInChildren<Transform>(true);
        for (int i = 0; i < chlids.Length; i++)
        {
            for (int j = i + 1; j < chlids.Length; j++)
            {
                if (chlids[i].transform.position == chlids[j].transform.position)
                {
                    DestroyImmediate(chlids[i].gameObject);
                    break;
                }
            }
        }
    }
    
}
