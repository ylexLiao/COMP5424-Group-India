using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Slow down", menuName = "Items/Slow Down")]
public class SlowdownPickup : Item
{
    public float slowdownFactor = 0.5f; // ��������
    public float duration = 10f; // ����ʱ��
                                 // Start is called before the first frame update
    public override void Use()
    {
        // ʵ�ָֻ�����ֵ��Ч��
        Debug.Log("ʹ���˼��ٵ��ߡ�");
        EnermyController[] enemies = FindObjectsOfType<EnermyController>();
        foreach (var enemy in enemies)
        {
            enemy.ApplySlowdown(slowdownFactor, duration);
        }

        Destroy(this); // ���ٵ���




    }
}
