using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : WeaponBase
{
    private void Start()
    {
        // ���ø�ͷ������
        weaponName = "Axe";
        damage = 25;
        range = 1.5f;
        attackCooldown = 1.0f;  // �������1��
    }

    // ʵ�ֹ�����Ϊ
    public override void Attack()
    {
        if (!canAttack)
        {
            Debug.Log("��ͷ������ȴ��...");
            return;
        }

        // ִ�и�ͷ�Ĺ����߼�
        Debug.Log("��ͷ�������Ե������ " + damage + " ���˺���");

        // ��ʼ������ȴ
        StartCoroutine(AttackCooldownRoutine());
    }
}
