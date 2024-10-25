using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
=======
using Valve.VR;
>>>>>>> 6a5cf8ae133fe4fdaa4e3fc831df2e410538351a

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private GameObject[] leftHandWeapons;   // ���������б�
    [SerializeField] private GameObject[] rightHandWeapons;  // ���������б�
    [SerializeField] private GameObject[] twoHandWeapons;    // ˫�������б�
    [SerializeField] private Transform leftHandMount;        // ���ֹ��ص�
    [SerializeField] private Transform rightHandMount;       // ���ֹ��ص�
    [SerializeField] private Transform twoHandMount;         // ˫���������ص�
<<<<<<< HEAD
=======
    [SerializeField] private Vector3 LrotationOffset;  // ��תƫ�� (��ŷ���Ƕȱ�ʾ)

    public SteamVR_Action_Boolean triggerAction; 
    public SteamVR_Input_Sources handType; 
>>>>>>> 6a5cf8ae133fe4fdaa4e3fc831df2e410538351a

    private int currentLeftWeaponIndex = 0;   // ��ǰ��������������
    private int currentRightWeaponIndex = 0;  // ��ǰ��������������
    private int currentTwoHandWeaponIndex = 0;// ��ǰ˫������������

    private bool isUsingTwoHandWeapon = false;  // ��ʶ�Ƿ�����ʹ��˫������

    void Start()
    {
        // ��ʼ��������Ĭ�Ϲ��ص�һ������
        InitializeWeapons();
    }

<<<<<<< HEAD
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
=======
    void Update()
    {
        if (triggerAction.GetStateDown(handType))
        {
            StartCoroutine(SwitchLeftHandWeapon());
            StartCoroutine(SwitchRightHandWeapon());
        }
    }

    /*void Update()
    {
>>>>>>> 6a5cf8ae133fe4fdaa4e3fc831df2e410538351a
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
<<<<<<< HEAD
    }
=======
    }*/
>>>>>>> 6a5cf8ae133fe4fdaa4e3fc831df2e410538351a


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

<<<<<<< HEAD

=======
>>>>>>> 6a5cf8ae133fe4fdaa4e3fc831df2e410538351a
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
<<<<<<< HEAD
    /*
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
        }*/

    void AttachWeaponToLeftHand(GameObject weapon)
    {
        if (weapon != null)
        {
            GameObject newWeapon = Instantiate(weapon, leftHandMount);
            newWeapon.transform.localPosition = Vector3.zero;
            newWeapon.transform.localRotation = Quaternion.identity;
            newWeapon.transform.SetParent(leftHandMount);  // ȷ������������Ϊ���ص���Ӷ���
=======

    void AttachWeaponToLeftHand(GameObject weapon)
    {
        Vector3 rotationOffset = new Vector3(70, 0, 180);
        Vector3 positionOffset = new Vector3(0, 0,-0.1f );  // (X, Y, Z)
        if (weapon != null)
        {
            GameObject newWeapon = Instantiate(weapon, leftHandMount);

            // ����λ��
            newWeapon.transform.position = leftHandMount.position;

            // ����λ�ò����ƫ��
            newWeapon.transform.position = leftHandMount.position + leftHandMount.rotation * positionOffset;

            // �ȶ����ֱ���ת���ٵ�����תƫ��
            newWeapon.transform.rotation = leftHandMount.rotation * Quaternion.Euler(rotationOffset);

>>>>>>> 6a5cf8ae133fe4fdaa4e3fc831df2e410538351a
            newWeapon.SetActive(true);
            Debug.Log("LeftHandWeapon attached: " + newWeapon.name);
        }
    }

    void AttachWeaponToRightHand(GameObject weapon)
    {
<<<<<<< HEAD
        if (weapon != null)
        {
            GameObject newWeapon = Instantiate(weapon, rightHandMount);
            newWeapon.transform.localPosition = Vector3.zero;
            newWeapon.transform.localRotation = Quaternion.identity;
            newWeapon.transform.SetParent(rightHandMount);  // ȷ������������Ϊ���ص���Ӷ���
=======
        Vector3 rotationOffset = new Vector3(70, 0, 0);
        Vector3 positionOffset = new Vector3(0, 0, -0.05f);
        if (weapon != null)
        {
            GameObject newWeapon = Instantiate(weapon, rightHandMount);

            if (newWeapon.CompareTag("Gun"))
            {
                // ����λ�ò����ƫ��
                newWeapon.transform.position = rightHandMount.position + rightHandMount.rotation * positionOffset;

                // ������ת�������תƫ��
                newWeapon.transform.rotation = rightHandMount.rotation * Quaternion.Euler(new Vector3(70, 0, 0));
            }

            if (newWeapon.CompareTag("Sword"))
            {
                // ����λ�ò����ƫ��
                newWeapon.transform.position = rightHandMount.position + rightHandMount.rotation * positionOffset;

                // ������ת�������תƫ��
                newWeapon.transform.rotation = rightHandMount.rotation * Quaternion.Euler(new Vector3(0, 90, 70));
            }


>>>>>>> 6a5cf8ae133fe4fdaa4e3fc831df2e410538351a
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
<<<<<<< HEAD
            newWeapon.transform.SetParent(twoHandMount);  // ȷ������������Ϊ���ص���Ӷ���
=======
>>>>>>> 6a5cf8ae133fe4fdaa4e3fc831df2e410538351a
            newWeapon.SetActive(true);
            Debug.Log("TwoHandWeapon attached: " + newWeapon.name);
        }
    }
<<<<<<< HEAD

=======
>>>>>>> 6a5cf8ae133fe4fdaa4e3fc831df2e410538351a
}
