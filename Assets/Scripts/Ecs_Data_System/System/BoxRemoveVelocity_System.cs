using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Physics;

/// <summary>
/// ʵ����GameObject֮��Ϳ��������׼���½���ʱ��
/// </summary>




public partial class BoxRemoveVelocity_System : SystemBase
{
    protected override void OnCreate()
    {
/*
        var entitymanager = World.DefaultGameObjectInjectionWorld.EntityManager;
        base.OnCreate();

        var boxquery = GetEntityQuery(typeof(Box_Data));
        var count = boxquery.CalculateEntityCount();
   
        var boxarray = boxquery.ToEntityArray(Unity.Collections.Allocator.Temp);
        for (int i = 0; i < count; i++)
        {
            entitymanager.RemoveComponent(boxarray[i], typeof(PhysicsVelocity));
           
        }
        boxarray.Dispose();*/
    }

    protected override void OnUpdate()
    {
        var beginCommandBuffer = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();
        var ecb = beginCommandBuffer.CreateCommandBuffer().AsParallelWriter();
        Entities.WithAll<PhysicsVelocity>().ForEach((Entity en, int entityInQueryIndex, in Box_Data data) =>
        {
            ecb.RemoveComponent<PhysicsVelocity>(entityInQueryIndex, en);
            
        }).ScheduleParallel();
        beginCommandBuffer.AddJobHandleForProducer(this.Dependency);
    }
}
