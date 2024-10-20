using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gun : MonoBehaviour
{
    [Header("Gun Settings")]
    public string weaponName = "Gun";     // ��������
    public int damage = 10;               // �����˺�
    public float range = 100f;            // �����Χ
    public float attackCooldown = 0.5f;   // ������ȴʱ��
    private float lastShotTime = 0;       // �ϴ������ʱ��

    [Header("Bullet Settings")]
    public GameObject bulletPrefab;       // �ӵ�Ԥ����
    public Transform firePoint;           // �ӵ������λ��
    public float bulletSpeed = 20f;       // �ӵ��ٶ�
    public int maxAmmo = 10;              // ���ҩ��
    private int currentAmmo;              // ��ǰ��ҩ��

    void Start()
    {
        currentAmmo = maxAmmo;            // ��ʼ����ҩ��
    }

    public void Shoot()
    {
        // ����Ƿ������ȴʱ��
        if (Time.time < lastShotTime + attackCooldown)
        {
            Debug.Log("Gun is on cooldown, cannot shoot yet.");
            return;
        }

        // ����Ƿ��е�ҩ
        if (currentAmmo <= 0)
        {
            Debug.Log("No ammo left! Reload required.");
            return;
        }

        // ���ٵ�ҩ
        currentAmmo--;
        Debug.Log("Shooting! Current ammo: " + currentAmmo);

        // ʵ�����ӵ�
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Debug.Log("Bullet instantiated at " + firePoint.position);

            // �����ӵ��ٶ�
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

        // �������һ�������ʱ��
        lastShotTime = Time.time;
    }

    // Reload function for testing
    public void Reload()
    {
        currentAmmo = maxAmmo;
        Debug.Log("Gun reloaded. Ammo refilled to " + maxAmmo);
    }
}
