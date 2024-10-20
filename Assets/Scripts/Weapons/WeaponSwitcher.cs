using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public GameObject[] weaponGroups;  // �洢����������
    public Transform leftHandMount;    // ���ֹ��ص�
    public Transform rightHandMount;   // ���ֹ��ص�

    private int currentWeaponGroup = 0;  // ��ǰ�����������

    void Start()
    {
        // �����ص��Ƿ�������ȷ
        if (leftHandMount == null)
        {
            Debug.LogError("LeftHandMount is not assigned!");
        }
        if (rightHandMount == null)
        {
            Debug.LogError("RightHandMount is not assigned!");
        }

        // �������������鲢��ʼ��
        for (int i = 0; i < weaponGroups.Length; i++)
        {
            weaponGroups[i].SetActive(false);
        }

        // Ĭ�����õ�һ�������鲢���ص��ֱ�
        ActivateWeaponGroup(currentWeaponGroup);
    }

    void Update()
    {
        // ʹ�� Oculus �ֱ���ť�����������л�
        if (OVRInput.GetDown(OVRInput.Button.One))  // A ��ť (����)
        {
            ActivateWeaponGroup(0);  // �л�����һ������ (��ǹ�Ͷ�)
        }
        if (OVRInput.GetDown(OVRInput.Button.Two))  // B ��ť (����)
        {
            ActivateWeaponGroup(1);  // �л����ڶ������� (�罣�Ͷ�)
        }
        if (OVRInput.GetDown(OVRInput.Button.Three))  // X ��ť (����)
        {
            ActivateWeaponGroup(2);  // �л������������� (˫�ִ�)
        }

        // ÿ֡������ֹ��ص��״̬
        if (rightHandMount.childCount > 0)
        {
            foreach (Transform child in rightHandMount)
            {
                Debug.Log("Right Hand Mount Child: " + child.name + ", Active: " + child.gameObject.activeSelf);
            }
        }
        else
        {
            Debug.LogWarning("RightHandMount has no child in this frame");
        }
    }

    void ActivateWeaponGroup(int index)
    {
        // ��������������
        for (int i = 0; i < weaponGroups.Length; i++)
        {
            weaponGroups[i].SetActive(false);
        }

        // ���ǰ������
        if (index >= 0 && index < weaponGroups.Length)
        {
            weaponGroups[index].SetActive(true);
            currentWeaponGroup = index;
            Debug.Log("Activated Weapon Group: " + index);
        }

        // ��ӡ��ǰ������������Ӷ�����Ϣ
        foreach (Transform weapon in weaponGroups[index].transform)
        {
            Debug.Log("Weapon found in group: " + weapon.name + ", Active: " + weapon.gameObject.activeSelf + ", Tag: " + weapon.tag);
        }

        // ��չ��ص�������Ӷ���
        foreach (Transform child in leftHandMount)
        {
            Destroy(child.gameObject);
        }
        Debug.Log("Cleared LeftHandMount");

        foreach (Transform child in rightHandMount)
        {
            Destroy(child.gameObject);
        }
        Debug.Log("Cleared RightHandMount");

        // ǿ�Ƽ����������е���������������
        foreach (Transform weapon in weaponGroups[index].transform)
        {
            Debug.Log("Processing weapon: " + weapon.name + ", Active: " + weapon.gameObject.activeSelf + ", Tag: " + weapon.tag);

            // ȷ����������״̬
            weapon.gameObject.SetActive(true);
            Debug.Log("Activated weapon: " + weapon.name);

            // ����ǩ����������
            if (weapon.CompareTag("LeftHandWeapon"))  // ��������
            {
                weapon.SetParent(leftHandMount);
                weapon.localPosition = Vector3.zero;
                weapon.localRotation = Quaternion.identity;
                Debug.Log("LeftHandWeapon attached: " + weapon.name);
            }
            else if (weapon.CompareTag("RightHandWeapon"))  // ��������
            {
                weapon.SetParent(rightHandMount);
                weapon.localPosition = Vector3.zero;
                weapon.localRotation = Quaternion.identity;
                Debug.Log("RightHandWeapon attached: " + weapon.name);
            }
            else
            {
                Debug.LogWarning("Weapon does not have correct tag or is not recognized: " + weapon.name);
            }
        }

        // ������ֺ����ֹ��ص��Ƿ���ȷ����������
        if (rightHandMount.childCount == 0)
        {
            Debug.LogWarning("No weapon attached to RightHandMount after activation!");
        }
        if (leftHandMount.childCount == 0)
        {
            Debug.LogWarning("No weapon attached to LeftHandMount after activation!");
        }
    }


}
