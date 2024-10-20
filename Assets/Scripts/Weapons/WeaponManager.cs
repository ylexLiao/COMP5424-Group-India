using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private GameObject[] leftHandWeapons;   // ���������б�
    [SerializeField] private GameObject[] rightHandWeapons;  // ���������б�
    [SerializeField] private GameObject[] twoHandWeapons;    // ˫�������б�
    [SerializeField] private Transform leftHandMount;        // ���ֹ��ص�
    [SerializeField] private Transform rightHandMount;       // ���ֹ��ص�
    [SerializeField] private Transform twoHandMount;         // ˫���������ص�

    private int currentLeftWeaponIndex = 0;   // ��ǰ��������������
    private int currentRightWeaponIndex = 0;  // ��ǰ��������������
    private int currentTwoHandWeaponIndex = 0;// ��ǰ˫������������

    private bool isUsingTwoHandWeapon = false;  // ��ʶ�Ƿ�����ʹ��˫������

    void Start()
    {
        // ��ʼ��������Ĭ�Ϲ��ص�һ������
        InitializeWeapons();
    }

    void Update()
    {
        // �л���������
        if (OVRInput.GetDown(OVRInput.Button.One))  // A ��ť (����)
        {
            if (!isUsingTwoHandWeapon)
            {
                SwitchLeftHandWeapon();
            }
        }

        // �л���������
        if (OVRInput.GetDown(OVRInput.Button.Two))  // B ��ť (����)
        {
            if (!isUsingTwoHandWeapon)
            {
                SwitchRightHandWeapon();
            }
        }

        // �л�˫������
        if (OVRInput.GetDown(OVRInput.Button.Three))  // X ��ť (����)
        {
            SwitchToTwoHandWeapon();
        }
    }

    void InitializeWeapons()
    {
        // ������������
        foreach (GameObject weapon in leftHandWeapons)
        {
            weapon.SetActive(false);
        }

        foreach (GameObject weapon in rightHandWeapons)
        {
            weapon.SetActive(false);
        }

        foreach (GameObject weapon in twoHandWeapons)
        {
            weapon.SetActive(false);
        }

        // Ĭ�ϼ����һ�����ֺ���������
        if (leftHandWeapons.Length > 0)
        {
            AttachWeaponToLeftHand(leftHandWeapons[currentLeftWeaponIndex]);
        }

        if (rightHandWeapons.Length > 0)
        {
            AttachWeaponToRightHand(rightHandWeapons[currentRightWeaponIndex]);
        }
    }

    void SwitchLeftHandWeapon()
    {
        if (isUsingTwoHandWeapon) return;

        if (leftHandMount.childCount > 0)
        {
            Destroy(leftHandMount.GetChild(0).gameObject);
        }

        currentLeftWeaponIndex = (currentLeftWeaponIndex + 1) % leftHandWeapons.Length;
        AttachWeaponToLeftHand(leftHandWeapons[currentLeftWeaponIndex]);
    }

    void SwitchRightHandWeapon()
    {
        if (isUsingTwoHandWeapon) return;

        if (rightHandMount.childCount > 0)
        {
            Destroy(rightHandMount.GetChild(0).gameObject);
        }

        currentRightWeaponIndex = (currentRightWeaponIndex + 1) % rightHandWeapons.Length;
        AttachWeaponToRightHand(rightHandWeapons[currentRightWeaponIndex]);
    }

    void SwitchToTwoHandWeapon()
    {
        // ж��������������
        if (leftHandMount.childCount > 0)
        {
            Destroy(leftHandMount.GetChild(0).gameObject);
        }
        if (rightHandMount.childCount > 0)
        {
            Destroy(rightHandMount.GetChild(0).gameObject);
        }

        // ж��˫�ֹ��ص��ϵ�����
        if (twoHandMount.childCount > 0)
        {
            Destroy(twoHandMount.GetChild(0).gameObject);
        }

        // ����˫������
        isUsingTwoHandWeapon = true;
        currentTwoHandWeaponIndex = (currentTwoHandWeaponIndex + 1) % twoHandWeapons.Length;
        AttachWeaponToTwoHand(twoHandWeapons[currentTwoHandWeaponIndex]);
    }

    void AttachWeaponToLeftHand(GameObject weapon)
    {
        if (weapon != null)
        {
            GameObject newWeapon = Instantiate(weapon, leftHandMount);
            newWeapon.transform.localPosition = Vector3.zero;
            newWeapon.transform.localRotation = Quaternion.identity;
            newWeapon.SetActive(true);
            Debug.Log("LeftHandWeapon attached: " + newWeapon.name);
        }
    }

    void AttachWeaponToRightHand(GameObject weapon)
    {
        if (weapon != null)
        {
            GameObject newWeapon = Instantiate(weapon, rightHandMount);
            newWeapon.transform.localPosition = Vector3.zero;
            newWeapon.transform.localRotation = Quaternion.identity;
            newWeapon.SetActive(true);
            Debug.Log("RightHandWeapon attached: " + newWeapon.name);
        }
    }

    void AttachWeaponToTwoHand(GameObject weapon)
    {
        if (weapon != null)
        {
            GameObject newWeapon = Instantiate(weapon, twoHandMount);
            newWeapon.transform.localPosition = Vector3.zero;
            newWeapon.transform.localRotation = Quaternion.identity;
            newWeapon.SetActive(true);
            Debug.Log("TwoHandWeapon attached: " + newWeapon.name);
        }
    }
}
