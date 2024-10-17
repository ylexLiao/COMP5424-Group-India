using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class WeaponBase : MonoBehaviour
{
    public string weaponName;       // ��������
    public int damage;              // �����˺�ֵ
    public float range;             // ����������Χ
    public float attackCooldown;    // ������ȴʱ��

    protected bool canAttack = true;  // �Ƿ���Թ���

    // ���󷽷���ÿ���������в�ͬ�Ĺ�����ʽ��������ʵ��
    public abstract void Attack();

    // ������ȴʱ��
    protected IEnumerator AttackCooldownRoutine()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
