using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

[GenerateAuthoringComponent]
public struct Award_Data : IComponentData
{
    
    public AwardType awardType;
    public int BulletNum;

}