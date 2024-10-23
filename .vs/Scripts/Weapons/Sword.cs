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
            Debug.Log("剑正在冷却中...");
            return;
        }

        Debug.Log("剑攻击！对敌人造成 " + damage + " 点伤害。");
        StartCoroutine(AttackCooldownRoutine());
    }
}

