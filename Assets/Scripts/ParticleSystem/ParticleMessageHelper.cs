
using UnityEngine;

//播放特效所需信息结构体
public class ParticleMessage
{
    public string effectName;
    public bool is_loop;
    public Transform parent;

    public ParticleMessage(string effectName, bool is_loop,Transform parent)
    {
        this.effectName = effectName;
        this.is_loop = is_loop;
        this.parent = parent;
    }
}
