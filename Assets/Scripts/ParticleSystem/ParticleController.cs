
using System;
using System.Collections;
using System.Collections.Generic;
using RougeFW;
using UnityEngine;


public enum EffectSourceType
{
    gunshot,
    
}

public class ParticleController : MonoBehaviour
{
    public static ParticleController instance;

    void Awake()
    {
        instance = this;


    }

    void Start()
    {
        RegistAllMsgAction();
    }

    void OnDestroy()
    {
        instance = null;
    }

    //public ParticleSourceItem template_particle;
    
    public List<ParticleSourceItem> particle_sources=new List<ParticleSourceItem>();

    public ParticleSourceItem GetParticleSource(ParticleSourceItem currentParticleSourceItem)
    {
        for (int i = 0; i < particle_sources.Count; i++)
            if (particle_sources[i].is_in_use == false && particle_sources[i].effectSourceType==currentParticleSourceItem.effectSourceType)
                return particle_sources[i];

        ParticleSourceItem particle_item = Instantiate(currentParticleSourceItem, transform);
        particle_sources.Add(particle_item);

        return particle_item;
    }

    public ParticleSourceItem PlayEffect(ParticleSourceItem particleSourceItem, bool is_loop)
    {
        ParticleSourceItem particle_item = GetParticleSource(particleSourceItem);

        particle_item.is_in_use = true;

        particle_item.gameObject.SetActive(true);
        
        ParticleSystem.MainModule tempModule=particle_item.effect.main;
        tempModule.loop = is_loop;
        
        particle_item.effect.Play();

        return particle_item;
    }

    
    public void PlayerEffectWithTransform(ParticleSourceItem particleSourceItem, Transform parent, bool is_loop,float duration = 3.0f)
    {
        ParticleSourceItem particle_item = PlayEffect(particleSourceItem, is_loop);

        particle_item.transform.parent = parent;
        particle_item.transform.localPosition = Vector3.zero;

        MsgSystem.instance.AddDelayAction(duration
            , () => 
            {
                particle_item.is_in_use = false;
                particle_item.gameObject.SetActive(false);
                particle_item.transform.parent = transform;
            }
            , UtilitySystem.GetRndomString(8));
        ;
        

    }
    
    public void StopEffect(ParticleSystem effect)
    {
        for (int i = 0; i < particle_sources.Count; i++)
        {
            if (particle_sources[i].effect == effect)
            {
                particle_sources[i].effect.Stop();

                particle_sources[i].is_in_use = false;
                particle_sources[i].gameObject.SetActive(false);
                particle_sources[i].transform.parent = transform;
            }
        }
    }

    private void RegistAllMsgAction()
    {
        MsgSystem.instance.RegistMsgAction(MsgSystem.left_gun_shot, OnGunShot);
        MsgSystem.instance.RegistMsgAction(MsgSystem.right_gun_shot, OnGunShot);
    }
    
    private void OnGunShot(System.Object[] objs)
    {

        if (objs[0] is ParticleMessage particleMessage)
        {
            TryPlayEffectWithTransform(particleMessage.effectName,particleMessage.is_loop,particleMessage.parent);
        }
        else
        {
        }

            
    }
    
    
    public ParticleInfo particleInfo;
    
    private void TryPlayEffectWithTransform(string name,bool is_loop,Transform parent)
    {
        particleInfo.effects.TryGetValue($"{name}", out ParticleSourceItem particleSourceItem);
        if (particleSourceItem == null)
        {
            Debug.LogError("尝试播放特效失败，资源不存在");
            return;
        }
        
        PlayerEffectWithTransform(particleSourceItem,parent,is_loop);
    }

    
}

