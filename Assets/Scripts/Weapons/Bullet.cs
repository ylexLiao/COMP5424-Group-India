using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10; // �ӵ��˺�ֵ

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("��⵽��ײ����ײ����" + other.name);

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("�ӵ��ɹ������˵��ˣ�" + other.name);

            HealthController health = other.GetComponent<HealthController>();
            if (health != null)
            {
                Debug.Log("�ӵ��Ե�����ɵ��˺�ֵ��" + damage);
                health.TakeDamage(damage);
            }
            else
            {
                Debug.LogWarning("���еĵ���û�� HealthController �����");
            }

            Destroy(gameObject); // �����ӵ�
        }
    }

}
