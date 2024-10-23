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

    /*    void Update()
        {
            // �л���������
            if (OVRInput.GetDown(OVRInput.Button.One))  // A ��ť ( ���� )
            {
                if (!isUsingTwoHandWeapon)
                {
                    StartCoroutine(SwitchLeftHandWeapon());
                }
            }

            // �л���������
            if (OVRInput.GetDown(OVRInput.Button.Two))  // B ��ť ( ���� )
            {
                if (!isUsingTwoHandWeapon)
                {
                    StartCoroutine(SwitchRightHandWeapon());
                }
            }

            // �л�˫������
            if (OVRInput.GetDown(OVRInput.Button.Three))  // X ��ť ( ���� )
            {
                StartCoroutine(SwitchToTwoHandWeapon());
            }
        }*/

    void Update()
    {
        // ʹ���������̰������в���
        if (Input.GetKeyDown(KeyCode.Alpha1))  // ʹ�ü����ϵ� 1 ��������С���̣�
        {
            Debug.Log("���̰��������ּ� 1���л�����������");
            if (!isUsingTwoHandWeapon)
            {
                StartCoroutine(SwitchLeftHandWeapon());
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))  // ʹ����ĸ�� Q ���в���
        {
            Debug.Log("���̰����� Q �����л�����������");
            if (!isUsingTwoHandWeapon)
            {
                StartCoroutine(SwitchRightHandWeapon());
            }
        }

        if (Input.GetKeyDown(KeyCode.E))  // ʹ����ĸ�� E ���в���
        {
            Debug.Log("���̰����� E �����л�˫��������");
            StartCoroutine(SwitchToTwoHandWeapon());
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

    IEnumerator SwitchLeftHandWeapon()
    {
        if (isUsingTwoHandWeapon) yield break;

        // ж�ص�ǰ��������
        if (leftHandMount.childCount > 0)
        {
            Destroy(leftHandMount.GetChild(0).gameObject);
        }

        // ���µ�ǰ����������������ȷ�������鳤����ѭ��
        currentLeftWeaponIndex = (currentLeftWeaponIndex + 1) % leftHandWeapons.Length;
        AttachWeaponToLeftHand(leftHandWeapons[currentLeftWeaponIndex]);
        yield return null;
    }

    IEnumerator SwitchRightHandWeapon()
    {
        if (isUsingTwoHandWeapon) yield break;

        // ж�ص�ǰ��������
        if (rightHandMount.childCount > 0)
        {
            Destroy(rightHandMount.GetChild(0).gameObject);
        }

        // ���µ�ǰ����������������ȷ�������鳤����ѭ��
        currentRightWeaponIndex = (currentRightWeaponIndex + 1) % rightHandWeapons.Length;
        AttachWeaponToRightHand(rightHandWeapons[currentRightWeaponIndex]);
        yield return null;
    }

    IEnumerator SwitchToTwoHandWeapon()
    {
        // ����Ѿ�ʹ��˫�����������лص�������
        if (isUsingTwoHandWeapon)
        {
            isUsingTwoHandWeapon = false;

            // ж�ص�ǰ˫������
            if (twoHandMount.childCount > 0)
            {
                Destroy(twoHandMount.GetChild(0).gameObject);
            }

            // ���¼������ֺ���������
            AttachWeaponToLeftHand(leftHandWeapons[currentLeftWeaponIndex]);
            AttachWeaponToRightHand(rightHandWeapons[currentRightWeaponIndex]);
        }
        else
        {
            // ж������������
            if (leftHandMount.childCount > 0)
            {
                Destroy(leftHandMount.GetChild(0).gameObject);
            }
            if (rightHandMount.childCount > 0)
            {
                Destroy(rightHandMount.GetChild(0).gameObject);
            }

            // �л���˫������
            isUsingTwoHandWeapon = true;
            currentTwoHandWeaponIndex = (currentTwoHandWeaponIndex + 1) % twoHandWeapons.Length;
            AttachWeaponToTwoHand(twoHandWeapons[currentTwoHandWeaponIndex]);
        }
        yield return null;
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
