using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowdownPickup : MonoBehaviour
{
    public float slowdownFactor = 0.5f; // ��������
    public float duration = 10f; // ����ʱ��

    private void OnTriggerEnter(Collider other)
    {
        // �����ײ�Ķ����Ƿ������
        if (other.CompareTag("Player"))
        {
            // ��ȡ���е��˲�Ӧ�ü���Ч��
            EnermyController[] enemies = FindObjectsOfType<EnermyController>();
            foreach (var enemy in enemies)
            {
                enemy.ApplySlowdown(slowdownFactor, duration);
            }

            Destroy(gameObject); // ���ٵ���
        }
    }
}