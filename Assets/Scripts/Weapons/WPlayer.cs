using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPlayer : MonoBehaviour
{
    public WeaponBase currentWeapon; // ��ǰ���е�����

    private void Update()
    {
        // ����������ִ�й���
        if (Input.GetMouseButtonDown(0) && currentWeapon != null)
        {
            currentWeapon.Attack();
        }

        // ����1���л�Ϊ��ͷ
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipWeapon("Axe");
        }

        // �����������Լ�����չ�����罣��������
    }

    // װ������
    public void EquipWeapon(string weaponName)
    {
        // ���ҳ��������е�����
        WeaponBase[] weapons = FindObjectsOfType<WeaponBase>();

        foreach (WeaponBase weapon in weapons)
        {
            if (weapon.weaponName == weaponName)
            {
                currentWeapon = weapon;
                Debug.Log("װ����������" + weaponName);
                break;
            }
        }

        if (currentWeapon == null)
        {
            Debug.LogWarning("δ�ҵ�ָ����������" + weaponName);
        }
    }
}
