using System;
using System.Collections;
using System.Collections.Generic;
using RougeFW;
using UnityEngine;

    public enum AudioSourceType
    {
        effect,
        bgm,
    }

    public class AudioSystem : MonoBehaviour
    {
        public static AudioSystem instance;

        
        void Awake()
        {
            instance = this;
            
            RegistItems();
            RegistAllMsgAction();

    }

        void Start()
        {
            /*RegistAllMsgAction();*/
        }

        void OnDestroy()
        {
            instance = null;
        }

        public AudioSourceItem template_effect;
        public AudioSourceItem template_bgm;

        public List<AudioSourceItem> audio_sources = new List<AudioSourceItem>();


        public void RegistItems()
        {
            /*audio_sources = new List<AudioSourceItem>(GetComponentsInChildren<AudioSourceItem>(true));*/
        }


        public AudioSourceItem GetAudioSource(AudioSourceType audio_type)
        {
        for (int i = 0; i < audio_sources.Count; i++)
            if (audio_sources[i].audio_type == audio_type && audio_sources[i].is_in_use == false)
                return audio_sources[i];
        AudioSourceItem audio_item = Instantiate(audio_type == AudioSourceType.effect ? template_effect : template_bgm, transform);
        audio_sources.Add(audio_item);
        return audio_item;
        /*if (audio_sources.Count < 20)
        {
            AudioSourceItem audio_item = Instantiate(audio_type == AudioSourceType.effect ? template_effect : template_bgm, transform);
            audio_sources.Add(audio_item);
            return audio_item;
        }
        else
        {
            for (int i = 0; i < audio_sources.Count; i++)
                
                if (audio_sources[i].audio_type == audio_type 
                    && audio_sources[i].audio_source.time > 0.5f 
                    && audio_sources[i].audio_source.clip != audioInfo.audio_clips[$"{BulletType.Bullet_Bomb}"])
                    return audio_sources[i];

            *//*return audio_type == AudioSourceType.effect ? template_effect : template_bgm;*//*
            return null;
        }*/
    }
     

        public AudioSourceItem PlayEffect(AudioClip clip, bool is_3d, bool is_loop,float volume)
        {

            AudioSourceItem audio_item = GetAudioSource(AudioSourceType.effect);

            audio_item.is_in_use = true;

            audio_item.gameObject.SetActive(true);

            audio_item.audio_source.spatialBlend = is_3d == true ? 1 : 0;
            audio_item.audio_source.loop = is_loop;

            audio_item.audio_source.volume = volume;

            audio_item.audio_source.clip = clip;
            audio_item.audio_source.Play();

            return audio_item;
        }



    public void PlayerEffect(AudioClip clip, Vector3 position, float duration = 3.0f, bool is_3d = true,float volume=1.0f)
    {
            AudioSourceItem audio_item = PlayEffect(clip, is_3d,false,volume); 
            
            audio_item.transform.position = position;
          
            MsgSystem.instance.AddDelayAction(duration
                , () => {
                    audio_item.is_in_use = false;
                    audio_item.gameObject.SetActive(false); 
                }
                , UtilitySystem.GetRndomString(8));
        }

    public void PlayerEffectWithTransform(AudioClip clip, Transform parent, float duration = 3.0f, float volume = 1.0f)
        {
            AudioSourceItem audio_item = PlayEffect(clip, true, false,volume);

            audio_item.transform.parent = parent;
            audio_item.transform.localPosition = Vector3.zero;

            MsgSystem.instance.AddDelayAction(duration
                , () => 
                {
                    audio_item.is_in_use = false;
                    audio_item.gameObject.SetActive(false);
                    audio_item.transform.parent = transform;
                }
                , UtilitySystem.GetRndomString(8)); 
        }


         public void PlayerLoopEffectWithTramsform(AudioClip clip, Transform parent, float volume = 1.0f)
        {
            AudioSourceItem audio_item = PlayEffect(clip, true, true,volume);

            audio_item.transform.parent = parent;
            audio_item.transform.localPosition = Vector3.zero;

        }

        public void StopEffect(AudioClip clip)
        {
            for (int i = 0; i < audio_sources.Count; i++)
            {
                if (audio_sources[i].audio_source.clip == clip)
                {
                    audio_sources[i].audio_source.Stop();

                    audio_sources[i].is_in_use = false;
                    audio_sources[i].gameObject.SetActive(false);
                    audio_sources[i].transform.parent = transform;
                }
            }
        }


        public void PlayBGM( AudioClip clip )
        {
            AudioSourceItem audio_item = GetAudioSource(AudioSourceType.bgm);
            audio_item.is_in_use = true;

            audio_item.gameObject.SetActive(true);
            audio_item.audio_source.clip = clip;
            audio_item.audio_source.Play();

        }


        public void StopAllBGM()
        {
            for (int i = 0; i < audio_sources.Count; i++)
                if (audio_sources[i].audio_type == AudioSourceType.bgm)
                    audio_sources[i].audio_source.Stop();
        }


        private void RegistAllMsgAction()
        {
            MsgSystem.instance.RegistMsgAction(MsgSystem.battle_ready, OnBattleReady);
            MsgSystem.instance.RegistMsgAction(MsgSystem.left_gun_shot, OnGunShot);
            MsgSystem.instance.RegistMsgAction(MsgSystem.right_gun_shot, OnGunShot);
            MsgSystem.instance.RegistMsgAction(MsgSystem.explosion, OnExplosion);
            MsgSystem.instance.RegistMsgAction(MsgSystem.dropitem, OnDrop);
        }

        private void OnBattleReady(System.Object[] objs)
        {
            
            //之后将获取到的objs转换为string获取关卡信息，根据信息播放bgm
            if (objs[0] is string name)
            {
                TryPlayBgm(name);
            }
            
        }
        
        
        
        
        public AudioInfo bgmAudioInfo;
        
        
        private void TryPlayBgm(string levelName)
        {
            bgmAudioInfo.audio_clips.TryGetValue($"{levelName}", out AudioClip clip);
            if (clip == null)
            {
                Debug.LogError("尝试播放bgm失败，音频资源不存在");
                return;
            }
            
            PlayBGM(clip);
        }

        
        private void OnGunShot(System.Object[] objs)
        {
           if (objs[0] is string name)
           {
               TryPlayShotAudio(name);
           }
        }

        private void OnExplosion(System.Object[] objs)
        {
            if (objs[0] is string name)
            {
               TryPlayExplosionAudio(name);
            }
        }

    private void TryPlayExplosionAudio(string name)
    {
        audioInfo.audio_clips.TryGetValue($"{name}", out AudioClip clip);
        if (clip == null)
        {
            Debug.LogError("尝试播放音效失败，音频资源不存在");
            return;
        }
        var audio_item = GameObject.Find("Template_explosion").GetComponent<AudioSourceItem>();
        audio_item.audio_source.clip = clip;
        audio_item.audio_source.Play();
        
    }

      private void OnDrop(System.Object[] objs)
        {
           if (objs[0] is string name && objs[1] is Vector3 position)
           {
              TryPlayDropAudio(name,position);
           }
        }

       private void TryPlayDropAudio(string audioName, Vector3 position)
       {
          audioInfo.audio_clips.TryGetValue($"{audioName}", out AudioClip clip);
          if (clip == null)
          {
             Debug.LogError("尝试播放音效失败，音频资源不存在");
             return;
          }

          PlayerEffect(clip,position);
       }

        public AudioInfo audioInfo;
        
        private void TryPlayShotAudio(string gunName)
        {
            audioInfo.audio_clips.TryGetValue($"{gunName}", out AudioClip clip);
            if (clip == null)
            {
                Debug.LogError("尝试播放音效失败，音频资源不存在");
                return;
            }
             
            PlayerEffect(clip,Vector3.zero);
        }       
    }


