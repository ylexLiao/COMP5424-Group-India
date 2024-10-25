using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : WeaponBase
{
    private void Start()
    {
        weaponName = "Sword";
        damage = 20;  // �����˺�ֵ
        range = 1.2f; // ������Χ
        attackCooldown = 0.8f;  // ������ȴʱ��
    }

    public override void Attack()
    {
        if (!canAttack)
        {
            Debug.Log("��������ȴ��...");
            return;
        }

        Debug.Log("��������׼���Ե������ " + damage + " ���˺���");

        // �������ͨ�����߼����߷�Χ�����Ѱ��Ŀ��
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, range);
        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                HealthController health = enemy.GetComponent<HealthController>();
                if (health != null)
                {
                    health.TakeDamage(damage);  // �Ե�������˺�
                    Debug.Log("���������е��ˣ��Ե������ " + damage + " ���˺���");
                }
            }
        }

        StartCoroutine(AttackCooldownRoutine()); // ������ȴʱ��
    }

    private void OnDrawGizmosSelected()
    {
        // ����������Χ���Ա��ڱ༭���п��ӻ�
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
