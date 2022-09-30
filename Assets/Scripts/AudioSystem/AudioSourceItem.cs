using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RougeFW
{

    public class AudioSourceItem : MonoBehaviour
    {
        public AudioSourceType audio_type = AudioSourceType.effect;
        public AudioSource audio_source;
        public bool is_in_use = false;

        private void Awake()
        {
            audio_source = transform.GetComponent<AudioSource>();
        }
    }
}

