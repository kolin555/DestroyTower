using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;


public partial class BalloonMove_System : SystemBase
{
    protected override void OnUpdate()
    {
        var deltaTime = Time.DeltaTime;
        var query = GetEntityQuery(typeof(Balloon_Data));
        var count = query.CalculateEntityCount();
        if (count > 0)
        {
            Entities.ForEach((ref Translation translation,in Balloon_Data data) =>
            {
                translation.Value.x+=deltaTime * data.moveDirection.x * data.moveVelocity;
                translation.Value.y+=deltaTime * data.moveDirection.y * data.moveVelocity;
                translation.Value.z+=deltaTime * data.moveDirection.z * data.moveVelocity;


            }).Run();

        }

        
    }

}
