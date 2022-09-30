using System.Collections;
using System.Collections.Generic;
using RougeFW;
using UnityEngine;
using Unity.Entities;
using Unity.Collections;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public partial class LevelContoller : MonoBehaviour
{
    public static LevelContoller levelInstance;

    public int tower_Id;
    

    //流程控制
    //时间
    /*public  float  time_Limite=60f;
    public  float  time_Down;*/

    public bool isJudged = false;
    public  bool startALevel = false;
    public bool isEnd = false;
    public bool isInit = false;
    /*public bool isFailed = false;*/
    //箱子个数
    public int target_BoxCount=1000;
    public int current_DestroyBoxCount;
    public int total_BoxCount=0;
    public int current_box_count = 0;

    //子弹个数


    public TowerInfo towerInfo;
    public Transform tower_point;



    //控制系统相关
    private EntityManager entityManager;
    private SimulationSystemGroup simulationSystemGroup;
    private BoxAddVelocity_System boxAddVelocity_System;
    private BoxRemoveVelocity_System boxRemoveVelocity_System;
    
    private BoxDestroy_System boxDestroy_System;
    private BoxEnabled_System boxEnabled_System;
    private BoxDisabled_System boxDisabled_System;

    private CollisionSystem collision_System;
    private ExplosionSystem explosion_System;
    private BalloonMove_System balloonMove_System;
    
    
    private World defaultWorld;


    private SaveItemCommon saveItemCommon;

    private void Awake()
    {
        levelInstance = this;

        tower_Id = 1;


        


        defaultWorld = World.DefaultGameObjectInjectionWorld;
        simulationSystemGroup = defaultWorld.GetOrCreateSystem<SimulationSystemGroup>();
        entityManager = defaultWorld.EntityManager;

        //将各系统加入默认系统组中
        boxAddVelocity_System = defaultWorld.GetOrCreateSystem<BoxAddVelocity_System>();
        simulationSystemGroup.AddSystemToUpdateList(boxAddVelocity_System);
        boxAddVelocity_System.Enabled = false;

        boxRemoveVelocity_System = defaultWorld.GetOrCreateSystem<BoxRemoveVelocity_System>();
        simulationSystemGroup.AddSystemToUpdateList(boxRemoveVelocity_System);
        boxRemoveVelocity_System.Enabled = false;

        boxDestroy_System = defaultWorld.GetOrCreateSystem<BoxDestroy_System>();
        simulationSystemGroup.AddSystemToUpdateList(boxDestroy_System);
        boxDestroy_System.Enabled = false;

        boxEnabled_System = defaultWorld.GetOrCreateSystem<BoxEnabled_System>();
        simulationSystemGroup.AddSystemToUpdateList(boxEnabled_System);
        boxEnabled_System.Enabled = false;

        boxDisabled_System = defaultWorld.GetOrCreateSystem<BoxDisabled_System>();
        simulationSystemGroup.AddSystemToUpdateList(boxDisabled_System);
        boxDisabled_System.Enabled = false;

        collision_System = defaultWorld.GetOrCreateSystem<CollisionSystem>();
        simulationSystemGroup.AddSystemToUpdateList(collision_System);
        collision_System.Enabled = false;
        
        explosion_System = defaultWorld.GetOrCreateSystem<ExplosionSystem>();
        simulationSystemGroup.AddSystemToUpdateList(explosion_System);
        explosion_System.Enabled = false;
        
        balloonMove_System = defaultWorld.GetOrCreateSystem<BalloonMove_System>();
        simulationSystemGroup.AddSystemToUpdateList(balloonMove_System);
        balloonMove_System.Enabled = false;
        

        defaultWorld.Update();
    }
  
    public void ReadLevelData()
    {
        SaveSystem.instance.ReadData("CommonData");

        if (saveItemCommon != null)
        {
            var data = saveItemCommon.common_data;
            if (data.TryGetValue("tower_id", out string tower_id))
            {
                tower_Id = int.Parse(tower_id);
            }
            else
            {
                data.Add("tower_id", "1");
            }


            if (data.TryGetValue("bullet_num_right", out string num_right_id))
            {
                BulletController.bulletInstance.currentRightPointId = int.Parse(num_right_id);
            }
            else
            {
                data.Add("bullet_num_right", "0");
            }


            if (data.TryGetValue("bullet_num_left", out string num_left_id))
            {
                BulletController.bulletInstance.currentLeftPointId = int.Parse(num_left_id);
            }
            else
            {
                data.Add("bullet_num_left", "0");
            }

            if (data.TryGetValue("bullet_mass_left", out string mass_left))
            {
                BulletController.bulletInstance.bulletMass_Left = int.Parse(mass_left);
            }
            else
            {
                data.Add("bullet_mass_left", "5");
            }
            if (data.TryGetValue("bullet_mass_right", out string mass_right))
            {
                BulletController.bulletInstance.bulletMass_Right = int.Parse(mass_right);
            }
            else
            {
                data.Add("bullet_mass_right", "5");
            }

            if (data.TryGetValue("bullet_velocity_left", out string velocity_left))
            {
                BulletController.bulletInstance.shootVelocity_Left = int.Parse(velocity_left);
            }
            else
            {
                data.Add("bullet_velocity_left", "100");
            }
            if (data.TryGetValue("bullet_velocity_right", out string velocity_right))
            {
                BulletController.bulletInstance.shootVelocity_Right = int.Parse(velocity_right);
            }
            else
            {
                data.Add("bullet_velocity_right", "100");
            }
        }
    }
    private void Start()
    {
        /*QueryBoxPlane();*/
        defaultWorld.Update();
        /*MsgSystem.instance.SendMsg("battle_ready", new object[]{ "level1" });*/

        saveItemCommon = SaveSystem.instance.GetSaveItem("CommonData") as SaveItemCommon;

        /*ReadLevelData();

        SaveSystem.instance.SaveData("CommonData");*/

    }
    private void Update()
    {
        //结束判断
        if (tower_Id == 10)
        {
            //显示游戏通关面板
            Application.Quit();
        }
    
        if(startALevel)
        {
            current_DestroyBoxCount = total_BoxCount - QueryBoxCount();
        }
       /* if (startALevel == false)
        {
                
            time_Down = time_Limite;
            
        }
        else
        {
            time_Down -= Time.deltaTime;
            time_Down = math.clamp(time_Down, 0, time_Limite);
            current_DestroyBoxCount = total_BoxCount - QueryBoxCount();

        }*/

        //计时结束
       /* if (time_Down == 0)
        {
            Lose();
            EndALevel();
        }*/
        //子弹射击完之后延迟五秒，判定胜负
        if (BulletController.bulletInstance.currentShootCount == 0)
        {

            BulletController.bulletInstance.currentShootCount = -1;
            if (isJudged)
            {
                return;
            }
            Sequence sequence = DOTween.Sequence();
            sequence.SetDelay(3f);
            sequence.AppendCallback(() =>
            {
                if (isEnd == true)
                {
                    return;
                }
                
                if(current_DestroyBoxCount < target_BoxCount)
                {
                    Lose();
                    EndALevel();
                }
                else
                {
                    Win();
                    
                    EndALevel();
                }
                
            });
             
        }


        if(startALevel==true && current_DestroyBoxCount >= target_BoxCount){
            Win();
            EndALevel();
            
        }


        current_box_count = QueryBoxCount();

    }
    private void OnDestroy()
    {
        levelInstance = null;
    }
    private void Win()
    {
        isJudged = true;
   
        UISystem.instance.ShowUIPanel("Award_Panel");
        /*time_Down = time_Limite;*/
        
    }
    private void Lose()
    {
        isJudged = true;
        UISystem.instance.ShowUIPanel("Failed_Panel");
        /*time_Down = time_Limite;*/
    }
 
    //计算组成塔的箱子的数量
    private int QueryBoxCount()
    {

        var query = new EntityQueryDesc
        {
            All = new ComponentType[] { typeof(Box_Data) },
            
            
        };
        EntityQuery box = entityManager.CreateEntityQuery(query);
        var count = box.CalculateEntityCount();
        box.Dispose();
        return count;
    }

    //查询到平台
    /* private bool QueryBoxPlane()
     {
         var query = new EntityQueryDesc
         {

             All = new ComponentType[] { typeof(BoxPlaneRise_Data) }
         };
         EntityQuery planes = entityManager.CreateEntityQuery(query);

         boxPlane = planes.GetSingletonEntity();
         if (boxPlane == null)
         {
             return false;
         }
         else
         {
             return true;
         }

     }*/
    //查询平台的Y值
    public void SaveLevelData() {

        var data = saveItemCommon.common_data;
        data["tower_id"] = tower_Id.ToString();
        data["bullet_num_right"] = BulletController.bulletInstance.currentRightPointId.ToString();
        data["bullet_num_left"] = BulletController.bulletInstance.currentLeftPointId.ToString();

        data["bullet_mass_left"] = BulletController.bulletInstance.bulletMass_Left.ToString();
        data["bullet_mass_right"] = BulletController.bulletInstance.bulletMass_Right.ToString();

        data["bullet_velocity_left"] = BulletController.bulletInstance.shootVelocity_Left.ToString();
        data["bullet_velocity_right"] = BulletController.bulletInstance.shootVelocity_Right.ToString();



        SaveSystem.instance.SaveData("CommonData");

    }

    //初始化一个关卡
    public void InitALevel(int id)
    {
        /*SaveLevelData();*/
        isInit = true;

        var tower = entityManager.Instantiate(towerInfo.tower_list[id - 1]);
        entityManager.SetName(tower, "tower" + id);
        /*entityManager.SetComponentData(tower, new Translation { Value = tower_point.transform.position });*/

        /*Instantiate(Resources.Load("Tower/Tower_" + id));*/
        /*towerInfo.tower_gameobject[id - 1].SetActive(true);
        Instantiate(towerInfo.tower_gameobject[id - 1]);*/

        World.DefaultGameObjectInjectionWorld.Update();
        total_BoxCount = QueryBoxCount();
        
        boxRemoveVelocity_System.Enabled = true;
        boxDisabled_System.Enabled = true;

        
        Debug.Log("InitALevel" + id);
        World.DefaultGameObjectInjectionWorld.Update();

        collision_System.Enabled = true;
        explosion_System.Enabled = true;
        balloonMove_System.Enabled = true;
        StartALevel();
    }
    //正式开始关卡
    public void StartALevel()
    {
        isInit = false;
        boxRemoveVelocity_System.Enabled = false;

        boxDisabled_System.Enabled = false;
        //逐层显示
        boxEnabled_System.Enabled = true;
        
        boxDestroy_System.Enabled = true;
        boxAddVelocity_System.Enabled = true;
        World.DefaultGameObjectInjectionWorld.Update();

        Sequence sequence = DOTween.Sequence();
        sequence.SetDelay(3f);
        sequence.AppendCallback(() =>
        {
            BulletController.bulletInstance.currentShootCount = 
                 BulletController.bulletInstance.maxShootCount;
            current_DestroyBoxCount = 0;
            /*UISystem.instance.ShowUIPanel("Process_Panel");*/
            isEnd = false;
        });

        BalloonSystem.balloonSystem.isCreatedBalloon = false;
        
        Debug.Log("StartALevel");
    }
    //关卡结束
    public void EndALevel()
    {

        /*BulletController.bulletInstance.currentShootCount = -1;*/
        /*UISystem.instance.HideUIPanel("Process_Panel");*/

        boxEnabled_System.Enabled = false;
        boxAddVelocity_System.Enabled = false;
        
        Debug.Log("EndALevel");
        
        World.DefaultGameObjectInjectionWorld.Update();
        startALevel = false;
        isEnd = true;
        

    }
    //关卡结束后下落到最低点的逻辑
    public void AfterALevel()
    {
        /*startALevel = false;*/
        //摧毁掉所有的箱子和子弹
        DestoryAll();

        isJudged = false;//判定胜负结束

        Debug.Log("AfterALevel");

        tower_Id++;
        World.DefaultGameObjectInjectionWorld.Update();

        //延迟一秒加载一座新的塔
        Sequence sequence = DOTween.Sequence();
        sequence.SetDelay(1f);
        sequence.AppendCallback(() =>
        {
            InitALevel(tower_Id);
        });
        
    }


    //关卡结束时摧毁所有的箱子以及子弹
    private void DestoryAll()
    {
        var box = entityManager.CreateEntityQuery(typeof(Box_Data));
        var array = box.ToEntityArray(Allocator.Temp);
        var count = box.CalculateEntityCount();
        for (int i = 0; i < count; i++)
        {
            entityManager.DestroyEntity(array[i]);
        }

        var bullet = entityManager.CreateEntityQuery(typeof(Bullet_Data));
        var bullet_array = bullet.ToEntityArray(Allocator.Temp);
        var bullet_count = bullet.CalculateEntityCount();
        for (int i = 0; i < bullet_count; i++)
        {
            entityManager.DestroyEntity(bullet_array[i]);
        }

        var balloon = entityManager.CreateEntityQuery(typeof(Balloon_Data));
        var balloon_array = balloon.ToEntityArray(Allocator.Temp);
        var balloon_count = balloon.CalculateEntityCount();
        for (int i = 0; i < balloon_count; i++)
        {
            entityManager.DestroyEntity(balloon_array[i]);
        }

        /*box.Dispose();
        bullet.Dispose();*/
        balloon.Dispose();
        World.DefaultGameObjectInjectionWorld.Update();
    }

}
