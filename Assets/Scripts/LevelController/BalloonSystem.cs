using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

public class BalloonSystem : MonoBehaviour
{
    public static BalloonSystem balloonSystem;

    public bool isCreatedBalloon;

    public float waitCreateBalloon;
    private float currentWaitTime;
    
    
    private BlobAssetStore blob;
    private EntityManager entityManager;
    private GameObjectConversionSettings settings;

    public Transform createPoint;
    public Entity currentBalloon;
    
    private void OnDestroy()
    {
        
        balloonSystem = null;
        blob.Dispose();
    }

    private void Awake()
    {
        balloonSystem = this;
        
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        blob = new BlobAssetStore();
        settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, blob);
        var balloonObject = Resources.Load<GameObject>("Balloon/Balloon");
        var balloonEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(balloonObject, settings);
        currentBalloon = balloonEntity;
    }

    private void Update()
    {
        if (!isCreatedBalloon)
        {
            currentWaitTime += Time.deltaTime;
            if (currentWaitTime >= waitCreateBalloon)
            {
                isCreatedBalloon = true;
                currentWaitTime = 0;
                CreateBalloon();
            }
        }
        


    }

    private void CreateBalloon()
    {
        Entity balloon = entityManager.Instantiate(currentBalloon);
        entityManager.SetName(balloon, "balloon");
        entityManager.SetComponentData(balloon, new Translation { Value = createPoint.transform.position });

        entityManager.SetComponentData(balloon, new Balloon_Data{moveDirection = new Vector3(1,0,0),moveVelocity = 1,awardType= AwardType.Bomb,BulletNum = 1});
        
        Debug.Log("生成气球");
        
    }
    
}
