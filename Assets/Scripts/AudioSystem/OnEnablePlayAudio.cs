using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RougeFW
{

    public class OnEnablePlayAudio : MonoBehaviour
    {

        public AudioClip audio_clip;
        public bool is_3d = false;

        private void OnEnable()
        {
            if(AudioSystem.instance != null ) 
                AudioSystem.instance.PlayerEffect(audio_clip, transform.position , 5, is_3d);
        }

    }

}
