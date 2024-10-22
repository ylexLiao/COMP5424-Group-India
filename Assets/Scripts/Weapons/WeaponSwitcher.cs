using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public GameObject[] weaponGroups;  // �洢����������
    private int currentWeaponGroup = 0;  // ��ǰ�����������

    void Start()
    {
        // ��������������
        for (int i = 0; i < weaponGroups.Length; i++)
        {
            weaponGroups[i].SetActive(false);
        }

        // Ĭ�����õ�һ��������
        ActivateWeaponGroup(currentWeaponGroup);
    }

    void Update()
    {
        // ʹ�� Oculus �ֱ���ť�����������л�
        if (OVRInput.GetDown(OVRInput.Button.One))  // A ��ť (����)
        {
            ActivateWeaponGroup(0);  // �л�����һ������ (���磺ǹ�Ͷ�)
        }
        if (OVRInput.GetDown(OVRInput.Button.Two))  // B ��ť (����)
        {
            ActivateWeaponGroup(1);  // �л����ڶ������� (���磺���Ͷ�)
        }
        if (OVRInput.GetDown(OVRInput.Button.Three))  // X ��ť (����)
        {
            ActivateWeaponGroup(2);  // �л������������� (˫�ִ�)
        }
    }

    void ActivateWeaponGroup(int index)
    {
        // ��������������
        for (int i = 0; i < weaponGroups.Length; i++)
        {
            weaponGroups[i].SetActive(false);  // �Ƚ�����������
        }

        // ����ָ����������
        if (index >= 0 && index < weaponGroups.Length)
        {
            weaponGroups[index].SetActive(true);  // ����ָ����������
            currentWeaponGroup = index;  // ���µ�ǰ����������
        }
    }
}
