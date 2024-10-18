using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gun : WeaponBase
{
    public GameObject bulletPrefab;   // 子弹的预制体
    public Transform firePoint;       // 子弹发射的位置
    public float bulletSpeed = 20f;   // 子弹速度
    public int maxAmmo = 10;          // 最大弹药数
    private int currentAmmo;          // 当前弹药

    private void Start()
    {
        currentAmmo = maxAmmo;        // 初始化弹药数
    }

    // 实现武器的攻击（射击）方法
    public override void Attack()
    {
        if (currentAmmo > 0)
        {
            Shoot();
            currentAmmo--;  // 减少弹药数
        }
        else
        {
            Debug.Log("弹药不足！");
        }
    }

    // 射击逻辑
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * bulletSpeed;  // 设置子弹的速度方向
    }
}
