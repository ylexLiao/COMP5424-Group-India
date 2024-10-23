using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    // 道具的名称
    public string itemName;

    // 道具的描述
    public string description;


    // 使用道具的方法
    public virtual void Use()
    {
        // 默认的使用效果，可以在子类中重写
        Debug.Log("使用了道具：" + itemName);
    }
}