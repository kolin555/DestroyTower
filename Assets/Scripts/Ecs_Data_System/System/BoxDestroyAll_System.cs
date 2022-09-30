using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Physics;
/// <summary>
/// 可以判断在平台升到-5的时候启用，需要Addlist
/// </summary>
/// 

[DisableAutoCreation]
public partial class BoxDestroyAll_System : SystemBase
{
    private EndSimulationEntityCommandBufferSystem bufferSystem;
    protected override void OnCreate()
    {
        base.OnCreate();
        bufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();

    }
    protected override void OnUpdate()
    {

        var ecb = bufferSystem.CreateCommandBuffer().AsParallelWriter();
        Entities.WithNone<PhysicsVelocity>().ForEach((Entity entity, int entityInQueryIndex,ref Box_Data boxdata, in Translation translation) =>
        {

                ecb.DestroyEntity(entityInQueryIndex, entity);

        }).ScheduleParallel();

        bufferSystem.AddJobHandleForProducer(this.Dependency);
    }
}
