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
public partial class LevelContoller : MonoBehaviour
{
   /* public static LevelContoller levelInstance;

    public int tower_Id;
    private Entity boxPlane;

    //流程控制
    private float time;

    private bool startAward = false;
    private bool startAlevel = false;
    //控制系统相关
    private EntityManager entityManager;
    private SimulationSystemGroup simulationSystemGroup;
    private BoxAddVelocity_System boxAddVelocity_System;
    private BoxRemoveVelocity_System boxRemoveVelocity_System;
    private BoxPlaneRise_System boxPlaneRise_System;
    private BoxPlaneDecline_System boxPlaneDecline_System;
    private BoxDestroy_System boxDestroy_System;
    private World defaultWorld;




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

        boxPlaneDecline_System = defaultWorld.GetOrCreateSystem<BoxPlaneDecline_System>();
        simulationSystemGroup.AddSystemToUpdateList(boxPlaneDecline_System);
        boxPlaneDecline_System.Enabled = false;

        boxPlaneRise_System = defaultWorld.GetOrCreateSystem<BoxPlaneRise_System>();
        simulationSystemGroup.AddSystemToUpdateList(boxPlaneRise_System);
        boxPlaneRise_System.Enabled = false;

        boxDestroy_System = defaultWorld.GetOrCreateSystem<BoxDestroy_System>();
        simulationSystemGroup.AddSystemToUpdateList(boxDestroy_System);
        boxDestroy_System.Enabled = false;


        defaultWorld.Update();
    }
    private void Start()
    {

        tower_Id = 1;
        time = 0;
        QueryBoxPlane();
        defaultWorld.Update();
    }
    private void Update()
    {
        //结束判断
        if (tower_Id == 4)
        {
            Application.Quit();
        }
        if (!QueryBoxPlane())
        {

            return;
        }
        var planeY = QueryPlaneTranslation_Y(boxPlane);

        if (planeY == -30)
        {
            if (boxPlaneDecline_System.Enabled == true)
            {
                AfterALevel();
            }
        }
        //控制上升

        if (planeY == -5)
        {
            if (boxPlaneRise_System.Enabled == true)
            {
                //关卡开始
                StartALevel();
                time = 0;
            }
            else
            {
                time += Time.deltaTime;
                time = math.clamp(time, 0, 60);
            }

            StartALevel();

            time += Time.deltaTime;
            time = math.clamp(time, 0, 60);

        }
        if (planeY == -5)
        {
            if (startAlevel == false)
            {
                StartALevel();
                time = 0;
            }
            else
            {
                time += Time.deltaTime;
                time = math.clamp(time, 0, 60);
            }

        }
        if (time == 60 || QueryBoxCount() == 0)
        {
            if (time == 60)
            {
                //Lose!!!
                Lose();
            }
            if (QueryBoxCount() == 0 && startAward == false)
            {

                Win();
            }
            *//* if (isEnd == false)
             {
                  EndALevel();
             }*//*

        }

    }
    private void OnDestroy()
    {
        levelInstance = null;
    }
    private void Win()
    {
        startAward = true;

        //UIManager.ui_Instance.ShowPanel<Award_Panel>();
        UISystem.instance.ShowUIPanel("Award_Panel");
    }
    private void Lose()
    {
        UISystem.instance.ShowUIPanel("Failed_Panel");
    }

    //计算组成塔的箱子的数量
    private int QueryBoxCount()
    {
        EntityQuery box = entityManager.CreateEntityQuery(typeof(Box_Data));
        var count = box.CalculateEntityCount();
        box.Dispose();
        return count;
    }
    //查询到平台
    private bool QueryBoxPlane()
    {
        var query = new EntityQueryDesc
        {

            All = new ComponentType[] { typeof(BoxPlaneRise_Data) }
        };
        EntityQuery planes = entityManager.CreateEntityQuery(query);

        if (planes.CalculateEntityCount() != 0)
        {
            NativeArray<Entity> temp = new NativeArray<Entity>(1, Allocator.Temp);
            var array = planes.ToEntityArray(Allocator.Temp);
            boxPlane = array[0];
            temp.Dispose();
            planes.Dispose();
            return true;
        }
        else
        {
            return false;
        }


    }
    //查询平台的Y值
    private float QueryPlaneTranslation_Y(Entity entity)
    {
        var trans = entityManager.GetComponentData<Translation>(entity).Value;
        return trans.y;
    }
    //初始化一个关卡
    public void InitALevel(int id)
    {
        if (id == 4)
        {
            Application.Quit();
            return;
        }
        Instantiate(Resources.Load("Tower/Tower_" + id));


        World.DefaultGameObjectInjectionWorld.Update();
        boxRemoveVelocity_System.Enabled = true;
        boxPlaneRise_System.Enabled = true;
        boxPlaneDecline_System.Enabled = false;


        Debug.Log("InitALevel" + id);
        World.DefaultGameObjectInjectionWorld.Update();

    }
    //正式开始关卡
    public void StartALevel()
    {
        boxRemoveVelocity_System.Enabled = false;
        boxPlaneRise_System.Enabled = false;
        boxAddVelocity_System.Enabled = true;

        startAlevel = true;

        boxDestroy_System.Enabled = true;
        World.DefaultGameObjectInjectionWorld.Update();
        Debug.Log("StartALevel");
    }
    //关卡结束
    public void EndALevel()
    {

        boxAddVelocity_System.Enabled = false;
        boxDestroy_System.Enabled = false;
        boxRemoveVelocity_System.Enabled = true;
        boxPlaneDecline_System.Enabled = true;
        Debug.Log("EndALevel");

        startAward = false;//奖励面板结束
        World.DefaultGameObjectInjectionWorld.Update();
    }
    //关卡结束后下落到最低点的逻辑
    public void AfterALevel()
    {



        //摧毁掉所有的箱子和平台
        DestoryAll();
        Debug.Log("AfterALevel");
        boxPlaneDecline_System.Enabled = false;

        time = 0;
        tower_Id++;
        World.DefaultGameObjectInjectionWorld.Update();
        InitALevel(tower_Id);
    }
    //关卡结束时摧毁所有的箱子以及平台
    private void DestoryAll()
    {
        EntityQuery box = entityManager.CreateEntityQuery(typeof(Box_Data));
        var array = box.ToEntityArray(Allocator.Temp);
        var count = box.CalculateEntityCount();
        for (int i = 0; i < count; i++)
        {
            entityManager.DestroyEntity(array[i]);
        }

        var buffer = entityManager.GetBuffer<Child>(boxPlane);

        var childrenArray = buffer.ToNativeArray(Allocator.Temp);
        var arrayCount = childrenArray.Length;
        for (int i = 0; i < arrayCount; i++)
        {
            entityManager.AddComponent<Disabled>(childrenArray[i].Value);

        }
        entityManager.AddComponent<Disabled>(boxPlane);

        childrenArray.Dispose();

        box.Dispose();
        World.DefaultGameObjectInjectionWorld.Update();
    }*/
}
