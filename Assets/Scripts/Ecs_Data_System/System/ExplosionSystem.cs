using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Extensions;
using Unity.Physics.Systems;
using Unity.Transforms;
using UnityEngine;

[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateBefore(typeof(BuildPhysicsWorld))]

    public partial class ExplosionSystem : SystemBase
    {
        EndFixedStepSimulationEntityCommandBufferSystem mEntityCommandBufferSystem;

        protected override void OnCreate()
        {
            mEntityCommandBufferSystem = World.GetOrCreateSystem<EndFixedStepSimulationEntityCommandBufferSystem>();
        }
        protected override void OnUpdate()
        {

            var bomb_query = GetEntityQuery(new EntityQueryDesc { All = new ComponentType[] { typeof(Explosion_Data),  typeof(Translation) } });

            if (bomb_query.CalculateEntityCount() == 0)
                return;

            var timeStep = Time.DeltaTime;
            var up = math.up();

            //var radius = 15.0f;
            //var force = 100000.0f;
            //var up_factor = 0.1f;
            //var jobHandle = Dependency;

            var commandBufferParallel = mEntityCommandBufferSystem.CreateCommandBuffer();

            var bomb_translations = bomb_query.ToComponentDataArray<Translation>(Allocator.TempJob);
            var bomb_Explosions = bomb_query.ToComponentDataArray<Explosion_Data>(Allocator.TempJob);

            MsgSystem.instance.SendMsg(MsgSystem.explosion, new object[] {$"{BulletType.Bullet_Bomb}" });
      
            var handle= Entities
                   .WithAll<Box_Data>()
                   .WithBurst()
                   .ForEach((Entity entity, ref PhysicsVelocity pv
                   , in PhysicsMass pm, in PhysicsCollider collider, in Translation pos, in Rotation rot) =>
                   {
                       for (int i = 0; i < bomb_translations.Length; i++)
                       {
                           var distance = math.distance(pos.Value, bomb_translations[i].Value);
                           var radius = bomb_Explosions[0].mExplosiveRange;
                           var force = bomb_Explosions[0].mExplosiveForce;
                           var up_factor = bomb_Explosions[0].mExplosiveIndex;
                           //Debug.Log("ex");
                           if (distance <= radius)
                               //pv.ApplyExplosionForce(pm, collider, pos, rot, force, bomb_translations[i].Value, distance, timeStep, up, up_factor);
                               pv.ApplyExplosionForce(pm, collider, pos, rot, force, bomb_translations[i].Value, radius, timeStep, up, up_factor);
                       }
                   }).WithDisposeOnCompletion(bomb_translations).WithDisposeOnCompletion(bomb_Explosions).Schedule(Dependency);
        //jobHandle.Complete();
           
           var finalHandle= Entities
                   .WithAny<Explosion_Data>()
                   .WithBurst()
                   .ForEach((Entity entity, in Translation pos) =>
                   {
                       
                       //commandBufferParallel.RemoveComponent<ExplosionComponent>(entity);
                       commandBufferParallel.AddComponent<Disabled>(entity);
                       //pos.Value=Vector3.zero;

                       //Vector3 position = pos.Value;
                       //Loom.QueueOnMainThread(() =>
                       //{
                       //    EffectManager.mInstance.mExplostionParticle.transform.position = position;
                       //    EffectManager.mInstance.mExplostionParticle.Play();
                       //});

                   }).Schedule(handle);
          
            Dependency = finalHandle;
            
        }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        
    }
}