using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHealth : MonoBehaviour
{
    public int health = 100;  // ���˵ĳ�ʼ����ֵ
    public static event System.Action OnEnemyKilled;

    // ���ٵ��˵�����ֵ
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    // �����������
    void Die()
    {
        OnEnemyKilled?.Invoke();
        Destroy(gameObject);  // ���ٵ��˶���
    }
}
