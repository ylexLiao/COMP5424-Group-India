using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class WeaponBase : MonoBehaviour
{
    public string weaponName;       // 武器名称
    public int damage;              // 武器伤害值
    public float range;             // 武器攻击范围
    public float attackCooldown;    // 攻击冷却时间

    protected bool canAttack = true;  // 是否可以攻击

    // 抽象方法：每种武器都有不同的攻击方式，由子类实现
    public abstract void Attack();

    // 处理冷却时间
    protected IEnumerator AttackCooldownRoutine()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
