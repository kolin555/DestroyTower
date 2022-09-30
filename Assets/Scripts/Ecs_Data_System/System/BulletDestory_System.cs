using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Physics;

public partial class BulletDestory_System : SystemBase
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
        Entities.ForEach((Entity entity, int entityInQueryIndex,in Bullet_Data data , in Translation translation) =>
        {
            if (translation.Value.y < -10)
            {
                ecb.DestroyEntity(entityInQueryIndex, entity);
            }


        }).ScheduleParallel();

        bufferSystem.AddJobHandleForProducer(this.Dependency);
    }
}
