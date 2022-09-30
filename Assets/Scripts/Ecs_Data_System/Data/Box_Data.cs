using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

[GenerateAuthoringComponent]
public struct Box_Data : IComponentData
{
    public bool isDestroy;
}
