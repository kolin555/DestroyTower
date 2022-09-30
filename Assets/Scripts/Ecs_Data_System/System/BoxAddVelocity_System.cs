using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;

/// <summary>
/// 当所有的都能看得见得时候加
/// </summary>

public partial class BoxAddVelocity_System : SystemBase
{
    
    protected override void OnStartRunning()
    {
        base.OnStartRunning();
        
    }
    protected override void OnUpdate()
    {
        var beginCommandBuffer = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();
        
        var ecb = beginCommandBuffer.CreateCommandBuffer().AsParallelWriter();

        var boxdisablequery = GetEntityQuery(typeof(Box_Data), typeof(Disabled));
        var count = boxdisablequery.CalculateEntityCount();
        
        Entities.WithNone<PhysicsVelocity>().ForEach((Entity en, int entityInQueryIndex, in Box_Data data) =>
        {
            if (count == 0)
            {
                ecb.AddComponent<PhysicsVelocity>(entityInQueryIndex, en);
                
            }

        }).ScheduleParallel();
        if (count == 0)
        {
            LevelContoller.levelInstance.startALevel = true;
           /* LevelContoller.levelInstance.isFailed = false;*/
        }
        
        
        beginCommandBuffer.AddJobHandleForProducer(Dependency);
    }
    
}
