using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gun : MonoBehaviour
{
    [Header("Gun Settings")]
    public string weaponName = "Gun";     // 武器名称
    public int damage = 10;               // 武器伤害
    public float range = 100f;            // 射击范围
    public float attackCooldown = 0.5f;   // 攻击冷却时间
    private float lastShotTime = 0;       // 上次射击的时间

    [Header("Bullet Settings")]
    public GameObject bulletPrefab;       // 子弹预制体
    public Transform firePoint;           // 子弹发射的位置
    public float bulletSpeed = 20f;       // 子弹速度
    public int maxAmmo = 10;              // 最大弹药数
    private int currentAmmo;              // 当前弹药数

    void Start()
    {
        currentAmmo = maxAmmo;            // 初始化弹药数
    }

    public void Shoot()
    {
        // 检查是否过了冷却时间
        if (Time.time < lastShotTime + attackCooldown)
        {
            Debug.Log("Gun is on cooldown, cannot shoot yet.");
            return;
        }

        // 检查是否有弹药
        if (currentAmmo <= 0)
        {
            Debug.Log("No ammo left! Reload required.");
            return;
        }

        // 减少弹药
        currentAmmo--;
        Debug.Log("Shooting! Current ammo: " + currentAmmo);

        // 实例化子弹
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Debug.Log("Bullet instantiated at " + firePoint.position);

            // 控制子弹速度
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = firePoint.forward * bulletSpeed;
                Debug.Log("Bullet fired with speed: " + bulletSpeed);
            }
            else
            {
                Debug.LogError("Bullet does not have a Rigidbody component!");
            }
        }
        else
        {
            Debug.LogError("BulletPrefab or FirePoint is not assigned!");
        }

        // 更新最后一次射击的时间
        lastShotTime = Time.time;
    }

    // Reload function for testing
    public void Reload()
    {
        currentAmmo = maxAmmo;
        Debug.Log("Gun reloaded. Ammo refilled to " + maxAmmo);
    }
}
