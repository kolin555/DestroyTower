using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public abstract class BasePanel : MonoBehaviour
{
    
 
    

    //是否开始显示
    private bool isShow;


    protected virtual void Awake()
    {
       
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Init();
    }

    /// <summary>
    /// 主要用于 初始化 按钮事件监听等等内容
    /// </summary>
    public abstract void Init();

    /// <summary>
    /// 显示自己时  做的事情
    /// </summary>
    public virtual void ShowMe()
    {
        
    }

    /// <summary>
    /// 隐藏自己时 做的事情
    /// </summary>
    public virtual void HideMe()
    {
        
        Time.timeScale = 1;
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}