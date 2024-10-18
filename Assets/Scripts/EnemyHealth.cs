using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 100;  // ���˵ĳ�ʼ����ֵ

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
        Destroy(gameObject);  // ���ٵ��˶���
    }
}
