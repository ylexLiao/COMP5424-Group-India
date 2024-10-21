using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private GameObject[] leftHandWeapons;   // 左手武器列表
    [SerializeField] private GameObject[] rightHandWeapons;  // 右手武器列表
    [SerializeField] private GameObject[] twoHandWeapons;    // 双手武器列表
    [SerializeField] private Transform leftHandMount;        // 左手挂载点
    [SerializeField] private Transform rightHandMount;       // 右手挂载点
    [SerializeField] private Transform twoHandMount;         // 双手武器挂载点

    private int currentLeftWeaponIndex = 0;   // 当前左手武器的索引
    private int currentRightWeaponIndex = 0;  // 当前右手武器的索引
    private int currentTwoHandWeaponIndex = 0;// 当前双手武器的索引

    private bool isUsingTwoHandWeapon = false;  // 标识是否正在使用双手武器

    void Start()
    {
        // 初始化武器，默认挂载第一把武器
        InitializeWeapons();
        Debug.Log("Connected Controller: " + OVRInput.GetConnectedControllers().ToString());
    }

    /* void Update()
     {
         // 切换左手武器
         if (OVRInput.GetDown(OVRInput.Button.One))  // A 按钮 (右手)
         {
             if (!isUsingTwoHandWeapon)
             {
                 StartCoroutine(SwitchLeftHandWeapon());
             }
         }

         // 切换右手武器
         if (OVRInput.GetDown(OVRInput.Button.Two))  // B 按钮 (右手)
         {
             if (!isUsingTwoHandWeapon)
             {
                 StartCoroutine(SwitchRightHandWeapon());
             }
         }

         // 切换双手武器
         if (OVRInput.GetDown(OVRInput.Button.Three))  // X 按钮 (左手)
         {
             StartCoroutine(SwitchToTwoHandWeapon());
         }
     }*/

/*    void Update()
    {
        // 测试 A 按钮输入
        if (OVRInput.GetDown(OVRInput.Button.One))  // A 按钮 (右手)
        {
            Debug.Log("A 按钮被按下！");
            if (!isUsingTwoHandWeapon)
            {
                StartCoroutine(SwitchLeftHandWeapon());
            }
        }

        // 测试 B 按钮输入
        if (OVRInput.GetDown(OVRInput.Button.Two))  // B 按钮 (右手)
        {
            Debug.Log("B 按钮被按下！");
            if (!isUsingTwoHandWeapon)
            {
                StartCoroutine(SwitchRightHandWeapon());
            }
        }

        // 测试 X 按钮输入
        if (OVRInput.GetDown(OVRInput.Button.Three))  // X 按钮 (左手)
        {
            Debug.Log("X 按钮被按下！");
            StartCoroutine(SwitchToTwoHandWeapon());
        }
    }*/

    void Update()
    {
        // 测试键盘输入
        if (Input.GetKeyDown(KeyCode.Alpha1)) // 使用键盘上的 1 键
        {
            Debug.Log("键盘按下了数字键 1（切换左手武器）");
            if (!isUsingTwoHandWeapon)
            {
                StartCoroutine(SwitchLeftHandWeapon());
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) // 使用键盘上的 2 键
        {
            Debug.Log("键盘按下了数字键 2（切换右手武器）");
            if (!isUsingTwoHandWeapon)
            {
                StartCoroutine(SwitchRightHandWeapon());
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) // 使用键盘上的 3 键
        {
            Debug.Log("键盘按下了数字键 3（切换双手武器）");
            StartCoroutine(SwitchToTwoHandWeapon());
        }
    }



    void InitializeWeapons()
    {
        // 禁用所有武器
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

        // 默认激活第一把左手和右手武器
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

        // 销毁当前左手武器
        if (leftHandMount.childCount > 0)
        {
            Destroy(leftHandMount.GetChild(0).gameObject);
        }

        yield return new WaitForEndOfFrame(); // 等待一帧，确保旧武器完全销毁

        // 切换到下一个左手武器
        currentLeftWeaponIndex = (currentLeftWeaponIndex + 1) % leftHandWeapons.Length;
        AttachWeaponToLeftHand(leftHandWeapons[currentLeftWeaponIndex]);
    }

    IEnumerator SwitchRightHandWeapon()
    {
        if (isUsingTwoHandWeapon) yield break;

        // 销毁当前右手武器
        if (rightHandMount.childCount > 0)
        {
            Destroy(rightHandMount.GetChild(0).gameObject);
        }

        yield return new WaitForEndOfFrame(); // 等待一帧，确保旧武器完全销毁

        // 切换到下一个右手武器
        currentRightWeaponIndex = (currentRightWeaponIndex + 1) % rightHandWeapons.Length;
        AttachWeaponToRightHand(rightHandWeapons[currentRightWeaponIndex]);
    }

    IEnumerator SwitchToTwoHandWeapon()
    {
        // 卸载所有现有武器
        if (leftHandMount.childCount > 0)
        {
            Destroy(leftHandMount.GetChild(0).gameObject);
        }
        if (rightHandMount.childCount > 0)
        {
            Destroy(rightHandMount.GetChild(0).gameObject);
        }

        // 卸载双手挂载点上的武器
        if (twoHandMount.childCount > 0)
        {
            Destroy(twoHandMount.GetChild(0).gameObject);
        }

        yield return new WaitForEndOfFrame(); // 等待一帧，确保旧武器完全销毁

        // 激活双手武器
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
