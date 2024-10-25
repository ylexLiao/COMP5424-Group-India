using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : WeaponBase
{
    private void Start()
    {
        weaponName = "Sword";
        damage = 20;  // 剑的伤害值
        range = 1.2f; // 攻击范围
        attackCooldown = 0.8f;  // 攻击冷却时间
    }

    public override void Attack()
    {
        if (!canAttack)
        {
            Debug.Log("剑正在冷却中...");
            return;
        }

        Debug.Log("剑攻击！准备对敌人造成 " + damage + " 点伤害。");

        // 这里可以通过射线检测或者范围检测来寻找目标
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, range);
        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                HealthController health = enemy.GetComponent<HealthController>();
                if (health != null)
                {
                    health.TakeDamage(damage);  // 对敌人造成伤害
                    Debug.Log("剑攻击击中敌人，对敌人造成 " + damage + " 点伤害。");
                }
            }
        }

        StartCoroutine(AttackCooldownRoutine()); // 启动冷却时间
    }

    private void OnDrawGizmosSelected()
    {
        // 画出攻击范围，以便在编辑器中可视化
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
