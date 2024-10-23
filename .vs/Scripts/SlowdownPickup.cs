using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Slow down", menuName = "Items/Slow Down")]
public class SlowdownPickup : Item
{
    public float slowdownFactor = 0.5f; // 减速因子
    public float duration = 10f; // 持续时间
                                 // Start is called before the first frame update
    public override void Use()
    {
        // 实现恢复生命值的效果
        Debug.Log("使用了减速道具。");
        EnermyController[] enemies = FindObjectsOfType<EnermyController>();
        foreach (var enemy in enemies)
        {
            enemy.ApplySlowdown(slowdownFactor, duration);
        }

        Destroy(this); // 销毁道具




    }
}
