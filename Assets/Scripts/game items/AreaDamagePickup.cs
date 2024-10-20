using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamagePickup : MonoBehaviour
{
    public float effectRadius = 10f; // 搜索范围
    public float damageRadius = 3f; // 伤害半径
    public int damageAmount = 50; // 伤害值

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyAreaDamage();
            Destroy(gameObject); // 销毁道具
        }
    }

    private void ApplyAreaDamage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, effectRadius);
        Vector3 bestPosition = Vector3.zero;
        int maxCount = 0;

        // 找到怪物聚集最多的区域
        foreach (var collider in hitColliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                Vector3 potentialPosition = collider.transform.position;
                int count = CountEnemiesInRadius(potentialPosition, damageRadius);

                if (count > maxCount)
                {
                    maxCount = count;
                    bestPosition = potentialPosition;
                }
            }
        }

        // 对最佳位置的怪物造成伤害
        if (maxCount > 0)
        {
            Collider[] damageColliders = Physics.OverlapSphere(bestPosition, damageRadius);
            foreach (var collider in damageColliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    EnemyHealth enemy = collider.GetComponent<EnemyHealth>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(damageAmount);
                    }
                }
            }
        }
    }

    private int CountEnemiesInRadius(Vector3 position, float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(position, radius);
        int count = 0;
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                count++;
            }
        }
        return count;
    }
}