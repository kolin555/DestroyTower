using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;

public class TowerInfo : MonoBehaviour
{
    public List<Entity> tower_list=new List<Entity>();


    public IList<GameObject> m_towers;


    public AssetLabelReference m_tower_label;

    public List<AssetReference> m_tower_list;


    private BlobAssetStore blob;
    private EntityManager entityManager;
    private GameObjectConversionSettings settings;


    private void Awake()
    {
         entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
         blob = new BlobAssetStore();
         settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, blob);
    }

    private void Start()
    {
        /*LoadTowersAsync();*/
        LoadTower();
        /*LoadSingleTower();*/
    }
    
    private void LoadTower()
    {
        for(int i = 0; i < 9; i++)     
        {
            var tower_object = Resources.Load<GameObject>("Tower/Tower_" + (i + 1));
            var tower_entity = GameObjectConversionUtility.ConvertGameObjectHierarchy(tower_object, settings);
            tower_list.Add(tower_entity);
        }
    }

    private void LoadSingleTower()
    {
        foreach(var tower in m_tower_list)
        {
            tower.LoadAssetAsync<GameObject>().Completed += OnLoadComplete;
        }
    }

    private void OnLoadComplete(AsyncOperationHandle<GameObject> obj)
    {
        var tower_entity = GameObjectConversionUtility.ConvertGameObjectHierarchy(obj.Result, settings);
        tower_list.Add(tower_entity);
    }

    private void LoadTowersAsync()
    {
        Addressables.LoadAssetsAsync<GameObject>(m_tower_label, null).Completed += OnResourcesRetrieved;
    }

    private void OnResourcesRetrieved(AsyncOperationHandle<IList<GameObject>> obj)
    {
        m_towers = obj.Result;
        foreach(var tower in m_towers)
        {
            var tower_entity = GameObjectConversionUtility.ConvertGameObjectHierarchy(tower, settings);
            tower_list.Add(tower_entity);
        }
    }

    private void OnDestroy()
    {
        blob.Dispose();
    }


}
