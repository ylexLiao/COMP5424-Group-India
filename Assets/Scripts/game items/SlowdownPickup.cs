using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowdownPickup : MonoBehaviour
{
    public float slowdownFactor = 0.5f; // 减速因子
    public float duration = 10f; // 持续时间

    private void OnTriggerEnter(Collider other)
    {
        // 检查碰撞的对象是否是玩家
        if (other.CompareTag("Player"))
        {
            // 获取所有敌人并应用减速效果
            EnermyController[] enemies = FindObjectsOfType<EnermyController>();
            foreach (var enemy in enemies)
            {
                enemy.ApplySlowdown(slowdownFactor, duration);
            }

            Destroy(gameObject); // 销毁道具
        }
    }
}