using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;

/// <summary>
/// 处理奖励
/// </summary>

public partial class Award_System : SystemBase
{

    
    protected override void OnUpdate()
    {
        var beginCommandBuffer = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();
        
        var ecb = beginCommandBuffer.CreateCommandBuffer().AsParallelWriter();
        Entities.WithAll<Award_Data>().ForEach((Entity en, int entityInQueryIndex, in Award_Data data) =>
        {

            BulletController.bulletInstance.OnShotBalloon(data.awardType,data.BulletNum);
            ecb.RemoveComponent<Award_Data>(entityInQueryIndex, en);
            ecb.AddComponent<Disabled>(entityInQueryIndex, en);
        }).WithoutBurst().Run();
        //beginCommandBuffer.AddJobHandleForProducer(Dependency);
        

        
    }
}