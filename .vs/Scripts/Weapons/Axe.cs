using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : WeaponBase
{
    private void Start()
    {
        // 设置斧头的属性
        weaponName = "Axe";
        damage = 25;
        range = 1.5f;
        attackCooldown = 1.0f;  // 攻击间隔1秒
    }

    // 实现攻击行为
    public override void Attack()
    {
        if (!canAttack)
        {
            Debug.Log("斧头正在冷却中...");
            return;
        }

        // 执行斧头的攻击逻辑
        Debug.Log("斧头攻击！对敌人造成 " + damage + " 点伤害。");

        // 开始攻击冷却
        StartCoroutine(AttackCooldownRoutine());
    }
}
