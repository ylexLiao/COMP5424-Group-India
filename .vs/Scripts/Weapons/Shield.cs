using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public int shieldHealth = 100;  // �����;ö�

    void OnCollisionEnter(Collision collision)
    {
        // ������˵Ĺ����� "EnemyProjectile" ��ǩ
        if (collision.gameObject.tag == "EnemyProjectile")
        {
            shieldHealth -= 10;  // ÿ�ι������ٶ��Ƶ��;ö�
            Debug.Log("Shield hit! Remaining health: " + shieldHealth);

            if (shieldHealth <= 0)
            {
                Debug.Log("Shield destroyed!");
                Destroy(gameObject);  // ���ٶ���
            }
        }
    }
}

