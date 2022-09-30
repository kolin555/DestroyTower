using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Physics;

public partial class BoxPlaneDecline_System : SystemBase
{
    protected override void OnUpdate()
    {
        var deltaTime = Time.DeltaTime;
        var query = GetEntityQuery(typeof(Box_Data));
        var count = query.CalculateEntityCount();
       /* 
        Entities.WithAll<BoxPlaneRise_Data>().ForEach((Entity en, int entityInQueryIndex, ref Translation translation, in BoxPlaneRise_Data data) =>
        {

            translation.Value.y -= deltaTime * data.riseVelocity;
            translation.Value.y = math.clamp(translation.Value.y, -30, -5);


        }).Run();*/

        Entities.WithNone<PhysicsVelocity>().ForEach((Entity en, int entityInQueryIndex, ref Translation translation, in Box_Data data) =>
        {
            translation.Value.y -= deltaTime;

        }).ScheduleParallel();

    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
