
using System;
using Unity.Entities;
using UnityEngine;
using Object = UnityEngine.Object;

[GenerateAuthoringComponent]
public struct Balloon_Data : IComponentData
{
    public float moveVelocity;
    
    public Vector3 moveDirection;


    public AwardType awardType;

    public int BulletNum;
}


public enum AwardType
{
    Null,
    GunUpdate,
    Bomb
}