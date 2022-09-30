using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RougeFW
{

    public class OnParticlePlayAudio : MonoBehaviour
    {

        public List<AudioClip> audio_clips;
        public bool is_3d = false;

        public void OnParticlePlay()
        {
            if (AudioSystem.instance != null)
            {
                for( int i = 0; i < audio_clips.Count; i ++ )
                    AudioSystem.instance.PlayerEffect(audio_clips[i], transform.position, 5, is_3d);
            }
        }
    }
}
