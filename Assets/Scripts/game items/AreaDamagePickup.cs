using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamagePickup : MonoBehaviour
{
    public float effectRadius = 10f; // ������Χ
    public float damageRadius = 3f; // �˺��뾶
    public int damageAmount = 50; // �˺�ֵ

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyAreaDamage();
            Destroy(gameObject); // ���ٵ���
        }
    }

    private void ApplyAreaDamage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, effectRadius);
        Vector3 bestPosition = Vector3.zero;
        int maxCount = 0;

        // �ҵ�����ۼ���������
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

        // �����λ�õĹ�������˺�
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