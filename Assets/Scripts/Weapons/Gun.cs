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
    public LineRenderer aimLine;          // ��׼��
    public Transform firePoint;           // ǹ��λ��
    public float aimDistance = 100f;      // ��׼�߳���

    [Header("Bullet Settings")]
    public GameObject bulletPrefab;       // �ӵ�Ԥ����
    public float bulletSpeed = 20f;       // �ӵ��ٶ�
    public int maxAmmo = 10;              // ���ҩ��
    private int currentAmmo;              // ��ǰ��ҩ��

    private bool isReloading = false;     // ��־�Ƿ���װ�ҩ
    public float reloadTime = 3f;         // װ��ʱ��

    void Start()
    {
        currentAmmo = maxAmmo;            // ��ʼ����ҩ��
    }

    void Update()
    {
        DrawAimLine();
    }

    void DrawAimLine()
    {
        // ��㣺ǹ��
        aimLine.SetPosition(0, firePoint.position);

        // �յ㣺ǹ����ǰ����һ������
        Vector3 endPosition = firePoint.position + firePoint.forward * aimDistance;
        aimLine.SetPosition(1, endPosition);
    }

    public void Shoot()
    {
        Debug.Log("Shoot method called!");  // ������Ϣ

        if (isReloading)
        {
            Debug.Log("Currently reloading, cannot shoot.");
            return;
        }

        // ����Ƿ������ȴʱ��
        if (Time.time < lastShotTime + attackCooldown)
        {
            Debug.Log("Gun is on cooldown, cannot shoot yet.");
            return;
        }

        // ����Ƿ��е�ҩ
        if (currentAmmo <= 0)
        {
            Debug.Log("No ammo left! Reloading...");
            StartCoroutine(Reload());  // ��ʼװ��
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
    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading for " + reloadTime + " seconds...");

        yield return new WaitForSeconds(reloadTime);  // �ȴ�װ��ʱ��

        currentAmmo = maxAmmo;
        Debug.Log("Reload complete! Ammo refilled to " + maxAmmo);
        isReloading = false;
    }
}
