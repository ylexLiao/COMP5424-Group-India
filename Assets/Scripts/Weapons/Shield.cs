using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public int shieldHealth = 100;  // 盾牌耐久度

    void OnCollisionEnter(Collision collision)
    {
        // 假设敌人的攻击有 "EnemyProjectile" 标签
        if (collision.gameObject.tag == "EnemyProjectile")
        {
            shieldHealth -= 10;  // 每次攻击减少盾牌的耐久度
            Debug.Log("Shield hit! Remaining health: " + shieldHealth);

            if (shieldHealth <= 0)
            {
                Debug.Log("Shield destroyed!");
                Destroy(gameObject);  // 销毁盾牌
            }
        }
    }
}

