using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Collections;

[GenerateAuthoringComponent]
public class TowerPrefab : IComponentData
{
  
    public Entity prefab;
    public List<Transform> trans;
  
}
