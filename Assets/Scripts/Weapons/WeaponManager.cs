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
        Debug.Log("Connected Controller: " + OVRInput.GetConnectedControllers().ToString());
    }

    /* void Update()
     {
         // �л���������
         if (OVRInput.GetDown(OVRInput.Button.One))  // A ��ť (����)
         {
             if (!isUsingTwoHandWeapon)
             {
                 StartCoroutine(SwitchLeftHandWeapon());
             }
         }

         // �л���������
         if (OVRInput.GetDown(OVRInput.Button.Two))  // B ��ť (����)
         {
             if (!isUsingTwoHandWeapon)
             {
                 StartCoroutine(SwitchRightHandWeapon());
             }
         }

         // �л�˫������
         if (OVRInput.GetDown(OVRInput.Button.Three))  // X ��ť (����)
         {
             StartCoroutine(SwitchToTwoHandWeapon());
         }
     }*/

/*    void Update()
    {
        // ���� A ��ť����
        if (OVRInput.GetDown(OVRInput.Button.One))  // A ��ť (����)
        {
            Debug.Log("A ��ť�����£�");
            if (!isUsingTwoHandWeapon)
            {
                StartCoroutine(SwitchLeftHandWeapon());
            }
        }

        // ���� B ��ť����
        if (OVRInput.GetDown(OVRInput.Button.Two))  // B ��ť (����)
        {
            Debug.Log("B ��ť�����£�");
            if (!isUsingTwoHandWeapon)
            {
                StartCoroutine(SwitchRightHandWeapon());
            }
        }

        // ���� X ��ť����
        if (OVRInput.GetDown(OVRInput.Button.Three))  // X ��ť (����)
        {
            Debug.Log("X ��ť�����£�");
            StartCoroutine(SwitchToTwoHandWeapon());
        }
    }*/

    void Update()
    {
        // ���Լ�������
        if (Input.GetKeyDown(KeyCode.Alpha1)) // ʹ�ü����ϵ� 1 ��
        {
            Debug.Log("���̰��������ּ� 1���л�����������");
            if (!isUsingTwoHandWeapon)
            {
                StartCoroutine(SwitchLeftHandWeapon());
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) // ʹ�ü����ϵ� 2 ��
        {
            Debug.Log("���̰��������ּ� 2���л�����������");
            if (!isUsingTwoHandWeapon)
            {
                StartCoroutine(SwitchRightHandWeapon());
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) // ʹ�ü����ϵ� 3 ��
        {
            Debug.Log("���̰��������ּ� 3���л�˫��������");
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

        // ���ٵ�ǰ��������
        if (leftHandMount.childCount > 0)
        {
            Destroy(leftHandMount.GetChild(0).gameObject);
        }

        yield return new WaitForEndOfFrame(); // �ȴ�һ֡��ȷ����������ȫ����

        // �л�����һ����������
        currentLeftWeaponIndex = (currentLeftWeaponIndex + 1) % leftHandWeapons.Length;
        AttachWeaponToLeftHand(leftHandWeapons[currentLeftWeaponIndex]);
    }

    IEnumerator SwitchRightHandWeapon()
    {
        if (isUsingTwoHandWeapon) yield break;

        // ���ٵ�ǰ��������
        if (rightHandMount.childCount > 0)
        {
            Destroy(rightHandMount.GetChild(0).gameObject);
        }

        yield return new WaitForEndOfFrame(); // �ȴ�һ֡��ȷ����������ȫ����

        // �л�����һ����������
        currentRightWeaponIndex = (currentRightWeaponIndex + 1) % rightHandWeapons.Length;
        AttachWeaponToRightHand(rightHandWeapons[currentRightWeaponIndex]);
    }

    IEnumerator SwitchToTwoHandWeapon()
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

        yield return new WaitForEndOfFrame(); // �ȴ�һ֡��ȷ����������ȫ����

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
