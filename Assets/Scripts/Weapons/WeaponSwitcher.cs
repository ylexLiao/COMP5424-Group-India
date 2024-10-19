using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class WeaponSwitcher : MonoBehaviour
{
    public GameObject[] weaponGroups;  // �洢����������
    private int currentWeaponGroup = 0;  // ��ǰ�����������

    void Start()
    {
        // ��ʼ��ʱֻ�����һ��������
        ActivateWeaponGroup(currentWeaponGroup);
    }

    void Update()
    {
        // ʹ�� Oculus �ֱ���ť�����������л�
        if (OVRInput.GetDown(OVRInput.Button.One))  // A ��ť (����)
        {
            ActivateWeaponGroup(0);  // �л�����һ������ (Զ���������ܺ�ǹ)
        }
        if (OVRInput.GetDown(OVRInput.Button.Two))  // B ��ť (����)
        {
            ActivateWeaponGroup(1);  // �л����ڶ������� (��ս�������ܺͽ�)
        }
        if (OVRInput.GetDown(OVRInput.Button.Three))  // X ��ť (����)
        {
            ActivateWeaponGroup(2);  // �л������������� (˫�ִ�)
        }
    }

    void ActivateWeaponGroup(int index)
    {
        // �Ƚ�������������
        for (int i = 0; i < weaponGroups.Length; i++)
        {
            weaponGroups[i].SetActive(false);
        }

        // ����ָ����������
        if (index >= 0 && index < weaponGroups.Length)
        {
            weaponGroups[index].SetActive(true);
            currentWeaponGroup = index;  // ���µ�ǰ����������
        }
    }
}


