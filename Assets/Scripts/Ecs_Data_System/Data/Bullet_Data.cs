using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

[GenerateAuthoringComponent]
public struct Bullet_Data : IComponentData
{

    public bool mIsBomb;
}
