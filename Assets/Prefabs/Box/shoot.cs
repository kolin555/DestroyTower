using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Physics;
using Ray = UnityEngine.Ray;

public class shoot : MonoBehaviour
{
    private BlobAssetStore blob;
    private EntityManager entityManager;
    private GameObjectConversionSettings settings;
    private Entity bulletEntity;
    public Transform shoot_point;
    private void Awake()
    {
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        blob = new BlobAssetStore();
        settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, blob);
        var bulletObject = Resources.Load<GameObject>("Bullet/Bullet_Type/Bullet_" + 1);
         bulletEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(bulletObject, settings);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void ShootBullet()
    {
        if (Input.GetMouseButton(0) == true)
        {
            Ray aim_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            UnityEngine.RaycastHit hit;

            Vector3 aim_point = Vector3.zero;

            if (Physics.Raycast(aim_ray, out hit, 1000) == true)
                aim_point = hit.point;
            else
                aim_point = aim_ray.GetPoint(200);
            shoot_point.LookAt(aim_point);

            Entity bullet = entityManager.Instantiate(bulletEntity);
            entityManager.SetName(bullet, "bullet");
            entityManager.SetComponentData(bullet, new Translation { Value = shoot_point.transform.position });
            entityManager.AddComponentData(bullet,
                new PhysicsVelocity { Linear = shoot_point.transform.forward * 100 });
        }
    }
}
