using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

[GenerateAuthoringComponent]
public struct Explosion_Data : IComponentData
    {
        public float mExplosiveRange;
        public float mExplosiveForce;
        public float mExplosiveIndex;


    }

