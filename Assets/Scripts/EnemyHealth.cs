using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHealth : MonoBehaviour
{
    public int health = 100;  // 敌人的初始生命值
    public static event System.Action OnEnemyKilled;

    // 减少敌人的生命值
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    // 处理敌人死亡
    void Die()
    {
        OnEnemyKilled?.Invoke();
        Destroy(gameObject);  // 销毁敌人对象
    }
}
