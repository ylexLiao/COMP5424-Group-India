using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10;  // �ӵ����˺�

    void Start()
    {
        // 5����Զ������ӵ�����ֹ�ӵ����й�Զ��ʧδ����
        Destroy(gameObject, 5f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);  // �Ե�������˺�
                Debug.Log("Enemy hit! Dealt " + damage + " damage.");
            }
            else
            {
                Debug.LogError("Enemy does not have EnemyHealth component!");
            }
        }

        // �����κ�����������ӵ�
        Destroy(gameObject);
    }
}
