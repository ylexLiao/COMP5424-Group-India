using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    // ���ߵ�����
    public string itemName;

    // ���ߵ�����
    public string description;


    // ʹ�õ��ߵķ���
    public virtual void Use()
    {
        // Ĭ�ϵ�ʹ��Ч������������������д
        Debug.Log("ʹ���˵��ߣ�" + itemName);
    }
}