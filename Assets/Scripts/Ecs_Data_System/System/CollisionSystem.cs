using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Burst;
using Unity.Collections;

[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
[UpdateAfter(typeof(StepPhysicsWorld)), UpdateBefore(typeof(EndFramePhysicsSystem))]
public partial class CollisionSystem : SystemBase
    {

        StepPhysicsWorld mStepPhysicsWorldSystem;
        EntityQuery mCollisionGroup;
        EndFixedStepSimulationEntityCommandBufferSystem mEntityCommandBufferSystem;

        protected override void OnCreate()
        {
            mStepPhysicsWorldSystem = World.GetOrCreateSystem<StepPhysicsWorld>();
            mCollisionGroup = GetEntityQuery(new EntityQueryDesc { All = new ComponentType[] { typeof(Bullet_Data) } });
            mEntityCommandBufferSystem = World.GetOrCreateSystem<EndFixedStepSimulationEntityCommandBufferSystem>();
        }

        protected override void OnStartRunning()
        {
            base.OnStartRunning();
            this.RegisterPhysicsRuntimeSystemReadWrite();
        }

        [BurstCompile]
        struct CollisionJob : ICollisionEventsJob
        {

            [ReadOnly] public ComponentDataFromEntity<Box_Data> mBoxComponent;
            [ReadOnly] public ComponentDataFromEntity<Balloon_Data> mBalloonComponent;
            [ReadOnly] public ComponentDataFromEntity<Bullet_Data> mBulletComponent;
            [ReadOnly] public ComponentDataFromEntity<Explosion_Data> mExplosionComponent;
            //[ReadOnly] public ComponentDataFromEntity<BulletBallComponent> mBulletBallComponent;
            //[ReadOnly] public ComponentDataFromEntity<MonsterComponentData> mMonsters;
            public EntityCommandBuffer mCommandBuffer;


       

        public  void Execute(CollisionEvent collisionEvent)
         {
           
            //处理爆炸
                Entity entityA = collisionEvent.EntityA;
                Entity entityB = collisionEvent.EntityB;

                bool isBodyAColl1 = mBoxComponent.HasComponent(entityA);
                bool isBodyBColl1 = mBulletComponent.HasComponent(entityB);
                if (isBodyAColl1 && isBodyBColl1)
                {
                    if (mBulletComponent[entityB].mIsBomb)
                    {
                    //mCommandBuffer.add
                    //mCommandBuffer.AddComponent<ExplosionComponent>(entityB);
                    
                    Explosion_Data explosionComponent = new Explosion_Data() 
                        { 
                        mExplosiveRange = 5.0f,
                        mExplosiveForce = 1000000.0f,
                        mExplosiveIndex = 0.1f,
                        };
                    
                    // mExplosionComponent[entityB] = explosionComponent;
                    mCommandBuffer.AddComponent<Explosion_Data>(entityB, explosionComponent);


                   
                    /* MsgSystem.instance.SendMsg(MsgSystem.left_gun_shot, new object[]
                     {
                         "boom"
                     });*/
                    //jobs不能使用burst访问全局变量
                    //播放一次爆炸的音效
                   }
                   else
                   {
                    //播放一次普通的音效
                    
                   }
                }
                bool isBodyAColl2 = mBulletComponent.HasComponent(entityA);
                bool isBodyBColl2 = mBoxComponent.HasComponent(entityB);
                if (isBodyAColl2 && isBodyBColl2)
                {
                
                    if (mBulletComponent[entityA].mIsBomb)
                    {
                        Explosion_Data explosionComponent = new Explosion_Data()
                        {
                            mExplosiveRange = 5.0f,
                            mExplosiveForce = 2000.0f,
                            mExplosiveIndex = 0.1f,
                            
                        };
                    

                    //mExplosionComponent[entityA] = explosionComponent;
                    mCommandBuffer.AddComponent<Explosion_Data>(entityA, explosionComponent);
                   
                    }
                else
                {
                   
                }
                }
                
                
                //处理气球
                bool isBodyAColl3 = mBalloonComponent.HasComponent(entityA);
                bool isBodyBColl3 = mBulletComponent.HasComponent(entityB);
                if (isBodyAColl3 && isBodyBColl3)
                {
                    Debug.Log("获得奖励");
                    Balloon_Data balloonData;
                    mBalloonComponent.TryGetComponent(entityA, out balloonData);
                    
                    Award_Data awardComponent = new Award_Data() 
                    {
                        awardType = balloonData.awardType,
                        BulletNum = balloonData.BulletNum
                    };

                    // mExplosionComponent[entityB] = explosionComponent;
                    mCommandBuffer.AddComponent<Award_Data>(entityA, awardComponent);

                    
                    
                    mCommandBuffer.RemoveComponent<Balloon_Data>(entityA);
                    
                    //mCommandBuffer.AddComponent<Disabled>(entityA,new Disabled());
                }
                bool isBodyAColl4 = mBulletComponent.HasComponent(entityA);
                bool isBodyBColl4 = mBalloonComponent.HasComponent(entityB);
                if (isBodyAColl4 && isBodyBColl4)
                {
                    Debug.Log("获得奖励");
                    
                    Balloon_Data balloonData;
                    mBalloonComponent.TryGetComponent(entityB, out balloonData);
                    
                    Award_Data awardComponent = new Award_Data() 
                    {
                        awardType = balloonData.awardType,
                        BulletNum = balloonData.BulletNum
                    };

                    // mExplosionComponent[entityB] = explosionComponent;
                    mCommandBuffer.AddComponent<Award_Data>(entityB, awardComponent);
                    
                    
                    mCommandBuffer.RemoveComponent<Balloon_Data>(entityB);
                }
                
                
            }

        


    }
        protected override void OnUpdate()
        {
            if (mCollisionGroup.CalculateEntityCount() == 0)
                return;
        //Debug.Log("Coll");
        //throw new System.NotImplementedException();
        CollisionJob job = new CollisionJob
        {
            //mTriggerComponet = GetComponentDataFromEntity<TriggerComponetData>(true),
            mBulletComponent = GetComponentDataFromEntity<Bullet_Data>(true),
            mBalloonComponent = GetComponentDataFromEntity<Balloon_Data>(true),
            mBoxComponent = GetComponentDataFromEntity<Box_Data>(true),
            mExplosionComponent = GetComponentDataFromEntity<Explosion_Data>(true),
            //mMonsters = GetComponentDataFromEntity<MonsterComponentData>(true),
            //mBulletBallComponent = GetComponentDataFromEntity<BulletBallComponent>(true),
            mCommandBuffer = mEntityCommandBufferSystem.CreateCommandBuffer(),
           
            
        };

        Dependency=job.Schedule(mStepPhysicsWorldSystem.Simulation, Dependency);
        
        /*
        Dependency =
           collectCollisionEventsJob.Schedule(mStepPhysicsWorldSystem.Simulation, Dependency);*/
        mEntityCommandBufferSystem.AddJobHandleForProducer(Dependency);

        


        }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        
        
    }

}


