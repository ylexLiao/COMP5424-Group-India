using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public GameObject[] weaponGroups;  // 存储所有武器组
    public Transform leftHandMount;    // 左手挂载点
    public Transform rightHandMount;   // 右手挂载点

    private int currentWeaponGroup = 0;  // 当前激活的武器组

    void Start()
    {
        // 检查挂载点是否设置正确
        if (leftHandMount == null)
        {
            Debug.LogError("LeftHandMount is not assigned!");
        }
        if (rightHandMount == null)
        {
            Debug.LogError("RightHandMount is not assigned!");
        }

        // 禁用所有武器组并初始化
        for (int i = 0; i < weaponGroups.Length; i++)
        {
            weaponGroups[i].SetActive(false);
        }

        // 默认启用第一个武器组并挂载到手柄
        ActivateWeaponGroup(currentWeaponGroup);
    }

    void Update()
    {
        // 使用 Oculus 手柄按钮进行武器组切换
        if (OVRInput.GetDown(OVRInput.Button.One))  // A 按钮 (右手)
        {
            ActivateWeaponGroup(0);  // 切换到第一组武器 (如枪和盾)
        }
        if (OVRInput.GetDown(OVRInput.Button.Two))  // B 按钮 (右手)
        {
            ActivateWeaponGroup(1);  // 切换到第二组武器 (如剑和盾)
        }
        if (OVRInput.GetDown(OVRInput.Button.Three))  // X 按钮 (左手)
        {
            ActivateWeaponGroup(2);  // 切换到第三组武器 (双手大剑)
        }

        // 每帧检查右手挂载点的状态
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
        // 禁用所有武器组
        for (int i = 0; i < weaponGroups.Length; i++)
        {
            weaponGroups[i].SetActive(false);
        }

        // 激活当前武器组
        if (index >= 0 && index < weaponGroups.Length)
        {
            weaponGroups[index].SetActive(true);
            currentWeaponGroup = index;
            Debug.Log("Activated Weapon Group: " + index);
        }

        // 打印当前武器组的所有子对象信息
        foreach (Transform weapon in weaponGroups[index].transform)
        {
            Debug.Log("Weapon found in group: " + weapon.name + ", Active: " + weapon.gameObject.activeSelf + ", Tag: " + weapon.tag);
        }

        // 清空挂载点的所有子对象
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

        // 强制激活武器组中的所有武器并挂载
        foreach (Transform weapon in weaponGroups[index].transform)
        {
            Debug.Log("Processing weapon: " + weapon.name + ", Active: " + weapon.gameObject.activeSelf + ", Tag: " + weapon.tag);

            // 确保武器激活状态
            weapon.gameObject.SetActive(true);
            Debug.Log("Activated weapon: " + weapon.name);

            // 检查标签并挂载武器
            if (weapon.CompareTag("LeftHandWeapon"))  // 左手武器
            {
                weapon.SetParent(leftHandMount);
                weapon.localPosition = Vector3.zero;
                weapon.localRotation = Quaternion.identity;
                Debug.Log("LeftHandWeapon attached: " + weapon.name);
            }
            else if (weapon.CompareTag("RightHandWeapon"))  // 右手武器
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

        // 检查右手和左手挂载点是否正确挂载了武器
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
