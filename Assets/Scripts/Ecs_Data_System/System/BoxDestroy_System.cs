using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Collections;
/// <summary>
/// 可以判断在平台升到-5的时候启用，需要Addlist
/// </summary>
/// 


public partial class BoxDestroy_System : SystemBase
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

        /*var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;*/

        /*Entities.ForEach((Entity entity,int entityInQueryIndex,
            ref Box_Data boxdata,in Translation translation,in PhysicsVelocity physicsVelocity) =>
        {
            if (translation.Value.y < -25)
            {
               ecb.DestroyEntity(entityInQueryIndex,entity);
            }
                

        }).ScheduleParallel(Dependency);

        bufferSystem.AddJobHandleForProducer(this.Dependency);*/
        var box_query = GetEntityQuery(new EntityQueryDesc { All = new ComponentType[] { typeof(Box_Data), typeof(Translation),typeof(PhysicsVelocity) } });

        if (box_query.CalculateEntityCount() == 0)
            return;

        var box_array = box_query.ToEntityArray(Allocator.TempJob);

        foreach(var box in box_array)
        {
            var translation = GetComponent<Translation>(box);
            

            var vec_x = math.clamp(translation.Value.x, -50, 100);
            var vec_y = math.clamp(translation.Value.y, -30, 30);
            var vec_z = math.clamp(translation.Value.z,  0, 100);


            var position = new Vector3(vec_x/10, vec_y/10, vec_z/10);
            

            /*if (physicVelocity.Linear.y > 30)
            {
                MsgSystem.instance.SendMsg(MsgSystem.dropitem, new object[] { $"Drop", position });
            }*/
            /*if (translation.Value.y < -25)
            {
                entityManager.DestroyEntity(box);

            }*/
            if (math.abs(vec_x) == 50 || vec_y == -30 || math.abs(vec_z) == 100)
            {
                MsgSystem.instance.SendMsg(MsgSystem.dropitem, new object[] { $"Drop", position });
                /*entityManager.DestroyEntity(box);*/
                ecb.DestroyEntity(box.Index, box);
            }
           /* if (physicVelocity.Linear.y > 30)
            {
                MsgSystem.instance.SendMsg(MsgSystem.dropitem, new object[] { $"Drop", position });
            }*/
            
          

        }

        box_array.Dispose();


    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

}
