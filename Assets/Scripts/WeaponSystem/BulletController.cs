 using System.Collections;
using System.Collections.Generic;
 using RougeFW;
 using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Physics;




public enum BulletType
{
    Bullet_Normal,
    Bullet_Bomb,
}


public enum BulletNumType
 {
     Bullet_Spawn_Nine,
 }

public class BulletController : MonoBehaviour
{
    public static BulletController bulletInstance;

    //射击次数限制
    public int maxShootCount = 10;
    public int currentShootCount=10;


    //球的速度

    public int shootVelocity_Right=100;

    public int shootVelocity_Left=100;

    public int shootVelocity_Max=1000;

    public int velocity_add_padding = 100;

    //球的质量

    public int bulletMass_Right=5;

    public int bulletMass_Left=5;

    public int bulletMass_Max=100;

    public int mass_add_padding = 5;

    //右手子弹个数控制
    public List<GameObject> bulletPointRightList = new List<GameObject>();
    public int currentRightPointId=0;
    public GameObject currentRightPoint;

    //右手子弹类型控制
    public List<Entity> bulletTypeRightList = new List<Entity>();
    public int currentRightTypeId = 0;
    public Entity currentRightType;

    //左手子弹个数控制
    public List<GameObject> bulletPointLeftList = new List<GameObject>();
    public int currentLeftPointId = 0;
    public GameObject currentLeftPoint;

    //左手子弹类型控制
    public List<Entity> bulletTypeLeftList = new List<Entity>();
    public int currentLeftTypeId = 0;
    public Entity currentLeftType;

    private BlobAssetStore blob;
    private EntityManager entityManager;
    private GameObjectConversionSettings settings;

    
    void OnDestroy()
    {
        bulletInstance = null;
        blob.Dispose();
        
    }
    private void Awake()
    {
        bulletInstance = this;

        //右手子弹个数控制
        var bulletRightPoints= GameObject.FindGameObjectWithTag("RightBullet");
        foreach (Transform point in bulletRightPoints.transform)
        {
            bulletPointRightList.Add(point.gameObject);
        }
        currentRightPoint = bulletPointRightList[0];

        //左手子弹个数控制
        var bulletLeftPoints = GameObject.FindGameObjectWithTag("LeftBullet");
        foreach (Transform point in bulletLeftPoints.transform)
        {
            bulletPointLeftList.Add(point.gameObject);
        }
        currentLeftPoint = bulletPointLeftList[0];




        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        blob = new BlobAssetStore();
        settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, blob);
        
        for (int i = 0; i < 2; i++)
        {
            var bulletObject = Resources.Load<GameObject>("Bullet/Bullet_Type/Bullet_" + (i + 1));
            var bulletEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(bulletObject, settings);

            entityManager.SetName(bulletEntity, bulletObject.name.Split('(')[0]);

            Debug.Log(bulletObject.ToString());
            /*if (entityManager.GetEnabled(bulletEntity))
            {
                Debug.Log("true");
            }*/

            bulletTypeRightList.Add(bulletEntity);
            bulletTypeLeftList.Add(bulletEntity);
            
        }
        currentRightType = bulletTypeRightList[0];
        currentLeftType = bulletTypeLeftList[0];
    }
    //读档
    private void Start()
    {

        //子弹类型控制
        

    }
    /*public void AddBulletTypeToLeftList(string name)
    {
        
        for (int i = 0; i < bulletTypeLeftList.Count; i++)
        {
            if (name == entityManager.GetName(bulletTypeLeftList[i]))
            {
                currentLeftType = bulletTypeLeftList[i];
                currentLeftTypeId = i;
                return;
            }
        }
        var leftbulletObj = Resources.Load<GameObject>("Bullet/Bullet_Type/" + name);
        var bulletEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(leftbulletObj, settings);

        entityManager.SetName(bulletEntity, leftbulletObj.name.Split('(')[0]);

        bulletTypeLeftList.Add(bulletEntity);
        currentLeftType = bulletEntity;
        currentLeftTypeId = bulletTypeLeftList.Count - 1;
    }*/
    public void AddBulletNumberToLeftList(string name)
    {
        Debug.Log(name);

        for (int i = 0; i < bulletPointLeftList.Count; i++)
        {
            if (name == bulletPointLeftList[i].name)
            {
                currentLeftPoint = bulletPointLeftList[i];
                currentLeftPointId = i;
                return;
            }
        }

        var leftbulletNumber = Instantiate(Resources.Load<GameObject>("Bullet/Bullet_Point/"+name));
        leftbulletNumber.name = leftbulletNumber.name.Split('(')[0];

        var leftbullet = GameObject.FindGameObjectWithTag("LeftBullet");
        leftbulletNumber.transform.SetParent(leftbullet.transform, false);
        bulletPointLeftList.Add(leftbulletNumber);

        currentLeftPoint = leftbulletNumber;
        currentLeftPointId = bulletPointLeftList.Count - 1;
    }



   /* public void AddBulletTypeToRightList(string name)
    {
        for(int i = 0; i < bulletTypeRightList.Count; i++)
        {
            if (name == entityManager.GetName(bulletTypeRightList[i]))
            {
                currentRightType = bulletTypeRightList[i];
                currentRightTypeId = i;
                return;
            }
        }

        var rightbulletObj = Resources.Load<GameObject>("Bullet/Bullet_Type/" + name);
        var bulletEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(rightbulletObj, settings);

        entityManager.SetName(bulletEntity, rightbulletObj.name.Split('(')[0]);

        bulletTypeRightList.Add(bulletEntity);
        currentRightType = bulletEntity;
        currentRightTypeId = bulletTypeRightList.Count - 1;
    }*/

    public void AddBulletNumberToRightList(string name)
    {
        for (int i = 0; i < bulletPointRightList.Count; i++)
        {
            if (name == bulletPointRightList[i].name)
            {
                currentRightPoint = bulletPointRightList[i];
                currentRightPointId = i;
                return;
            }
        }


        var rightbulletNumber = Instantiate(Resources.Load<GameObject>("Bullet/Bullet_Point/" + name));
        rightbulletNumber.name = rightbulletNumber.name.Split('(')[0];

        var rightbullet = GameObject.FindGameObjectWithTag("RightBullet");
        rightbulletNumber.transform.SetParent(rightbullet.transform, false);
        bulletPointRightList.Add(rightbulletNumber);

        currentRightPoint = rightbulletNumber;
        currentRightPointId = bulletPointRightList.Count - 1;
    }
   /* public void ChangeRightBulletType()
    {
        var id = currentRightTypeId;
        var count = bulletTypeRightList.Count;
        var index = (id == count - 1) ? 0 : id + 1;
        var type = bulletTypeRightList[index];
        currentRightType = type;
        currentRightTypeId = index;
    }*/
    public void ChangeRightBulletPoint()
    {
        var id = currentRightPointId;
        var count = bulletPointRightList.Count;

        /*var index = (id == count - 1) ? 0 : id + 1;*/
        var index = (id == count - 1) ? id : id + 1;
        var point = bulletPointRightList[index];
        currentRightPoint = point;
        currentRightPointId = index;
    }
   /* public void ChangeLeftBulletType()
    {
        var id = currentLeftTypeId;
        var count = bulletTypeLeftList.Count;
        var index = (id == count - 1) ? 0 : id + 1;
        var type = bulletTypeLeftList[index];
        currentLeftType = type;
        currentLeftTypeId = index;
    }*/
    public void ChangeLeftBulletPoint()
    {
        var id = currentLeftPointId;
        var count = bulletPointLeftList.Count;
        /*var index = (id == count - 1) ? 0 : id + 1;*/
        var index = (id == count - 1) ? id : id + 1;
        var point = bulletPointLeftList[index];
        currentLeftPoint = point;
        currentLeftPointId = index;
    }


    public void MassUp_Right()
    {
        bulletMass_Right =
            Mathf.Clamp(bulletMass_Right + mass_add_padding, 0, bulletMass_Max);
    }
    public void MassUp_Left()
    {
        bulletMass_Left =
            Mathf.Clamp(bulletMass_Left + mass_add_padding, 0, bulletMass_Max);
    }
    public void VelocityUp_Right()
    {
        shootVelocity_Right = 
            Mathf.Clamp(shootVelocity_Right + velocity_add_padding, 0, shootVelocity_Max);
    }
    public void VelocityUp_Left()
    {
        shootVelocity_Left =
            Mathf.Clamp(shootVelocity_Left + velocity_add_padding, 0, shootVelocity_Max);
    }



    private int awardShotNum;
    private AwardType currentAward;

    public void OnShotBalloon(AwardType awardType,int awardShotNum)
    {
        this.awardShotNum = awardShotNum;
        currentAward = awardType;
    }
    
    
    public void Spawn_Left()
    {


        if (LevelContoller.levelInstance.isJudged == true)
        {
            //在判定面板期间可以随意射击
            currentShootCount = maxShootCount;
        }
       
        if (currentShootCount <= 0)
        {
            return;
        }
        if (LevelContoller.levelInstance.startALevel == true)
        {
            currentShootCount--;
        }
        /*var isbomb = false;*/
        string bullet_type = $"{BulletType.Bullet_Normal}";

        MsgSystem.instance.SendMsg("left_gun_shot", new object[] { $"{GunController.gunInstance.currentLeftGun.name }" });
        foreach (Transform point in currentLeftPoint.transform)
        {

            currentLeftType = bulletTypeLeftList[0];
            currentLeftTypeId = 0;

            if (awardShotNum > 0)
            {
                switch (currentAward)
                {
                    case AwardType.Bomb:
                        currentLeftType = bulletTypeLeftList[1];
                        currentLeftTypeId = 1;
                        bullet_type = $"{BulletType.Bullet_Bomb}";
                        break;
                }
            }


            Entity bullet = entityManager.Instantiate(currentLeftType);
            entityManager.SetName(bullet, "bullet");

            entityManager.SetComponentData(bullet, new Translation { Value = point.transform.position });
            entityManager.AddComponentData(bullet,
                new PhysicsVelocity { Linear = point.transform.forward * shootVelocity_Left });
            var mass_data = entityManager.GetComponentData<PhysicsMass>(bullet);
            mass_data.InverseMass = 1 / (float)bulletMass_Left;
            entityManager.SetComponentData(bullet, mass_data);

        }
        
        if (awardShotNum>0)
            awardShotNum -= 1;


    
        
    }
        
        
    
    public void Spawn_Right()
    {

        
        if (LevelContoller.levelInstance.isJudged == true)
        {
            //在判定面板期间可以随意射击
            currentShootCount = maxShootCount;
        } 

        if (currentShootCount <= 0)
        {
            return;
        }

        if (LevelContoller.levelInstance.startALevel == true)
        {
            currentShootCount--;
        }
        string bullet_type = $"{BulletType.Bullet_Normal}";

        MsgSystem.instance.SendMsg("right_gun_shot", new object[] { $"{GunController.gunInstance.currentRightGun.name }" });
     
        foreach (Transform point in currentRightPoint.transform)
        {

            currentRightType = bulletTypeRightList[0];
            currentRightTypeId = 0;

            
            if (awardShotNum > 0)
            {
                switch (currentAward)
                {
                    case AwardType.Bomb:
                        currentRightType = bulletTypeRightList[1];
                        currentRightTypeId = 1;
                        bullet_type = $"{BulletType.Bullet_Bomb}";
                        break;
                }
            }


            Entity bullet = entityManager.Instantiate(currentRightType);
            entityManager.SetName(bullet, "bullet");

            entityManager.SetComponentData(bullet, new Translation { Value = point.transform.position });
            entityManager.AddComponentData(bullet,
                new PhysicsVelocity { Linear = point.transform.forward * shootVelocity_Right });
            var mass_data = entityManager.GetComponentData<PhysicsMass>(bullet);
            mass_data.InverseMass = 1 / (float)bulletMass_Right; 
            entityManager.SetComponentData(bullet, mass_data);

            
        }

        /*AudioSystem.instance.PlayEffect()*/

        if (awardShotNum>0)
            awardShotNum -= 1;

       
   
        
    }
}
