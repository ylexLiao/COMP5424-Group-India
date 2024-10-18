using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : WeaponBase
{
    private void Start()
    {
        weaponName = "Sword";
        damage = 20;
        range = 1.2f;
        attackCooldown = 0.8f;
    }

    public override void Attack()
    {
        if (!canAttack)
        {
            Debug.Log("��������ȴ��...");
            return;
        }

        Debug.Log("���������Ե������ " + damage + " ���˺���");
        StartCoroutine(AttackCooldownRoutine());
    }
}

