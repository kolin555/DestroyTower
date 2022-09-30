using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;

public partial class BoxDisabled_System : SystemBase
{
    protected override void OnUpdate()
    {
        var beginCommandBuffer = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();

        var ecb = beginCommandBuffer.CreateCommandBuffer().AsParallelWriter();
        Entities.WithNone<Disabled>().ForEach((Entity en, int entityInQueryIndex, in Box_Data data) =>
        {
            ecb.AddComponent<Disabled>(entityInQueryIndex, en);

        }).ScheduleParallel();
        beginCommandBuffer.AddJobHandleForProducer(Dependency);
    }

    
}
