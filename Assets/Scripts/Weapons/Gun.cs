using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gun : WeaponBase
{
    public GameObject bulletPrefab;   // �ӵ���Ԥ����
    public Transform firePoint;       // �ӵ������λ��
    public float bulletSpeed = 20f;   // �ӵ��ٶ�
    public int maxAmmo = 10;          // ���ҩ��
    private int currentAmmo;          // ��ǰ��ҩ

    private void Start()
    {
        currentAmmo = maxAmmo;        // ��ʼ����ҩ��
    }

    // ʵ�������Ĺ��������������
    public override void Attack()
    {
        if (currentAmmo > 0)
        {
            Shoot();
            currentAmmo--;  // ���ٵ�ҩ��
        }
        else
        {
            Debug.Log("��ҩ���㣡");
        }
    }

    // ����߼�
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * bulletSpeed;  // �����ӵ����ٶȷ���
    }
}
