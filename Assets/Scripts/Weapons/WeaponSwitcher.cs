using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public GameObject[] weaponGroups;  // 存储所有武器组
    private int currentWeaponGroup = 0;  // 当前激活的武器组

    void Start()
    {
        // 禁用所有武器组
        for (int i = 0; i < weaponGroups.Length; i++)
        {
            weaponGroups[i].SetActive(false);
        }

        // 默认启用第一个武器组
        ActivateWeaponGroup(currentWeaponGroup);
    }

    void Update()
    {
        // 使用 Oculus 手柄按钮进行武器组切换
        if (OVRInput.GetDown(OVRInput.Button.One))  // A 按钮 (右手)
        {
            ActivateWeaponGroup(0);  // 切换到第一组武器 (例如：枪和盾)
        }
        if (OVRInput.GetDown(OVRInput.Button.Two))  // B 按钮 (右手)
        {
            ActivateWeaponGroup(1);  // 切换到第二组武器 (例如：剑和盾)
        }
        if (OVRInput.GetDown(OVRInput.Button.Three))  // X 按钮 (左手)
        {
            ActivateWeaponGroup(2);  // 切换到第三组武器 (双手大剑)
        }
    }

    void ActivateWeaponGroup(int index)
    {
        // 禁用所有武器组
        for (int i = 0; i < weaponGroups.Length; i++)
        {
            weaponGroups[i].SetActive(false);  // 先禁用所有武器
        }

        // 启用指定的武器组
        if (index >= 0 && index < weaponGroups.Length)
        {
            weaponGroups[index].SetActive(true);  // 激活指定的武器组
            currentWeaponGroup = index;  // 更新当前武器组索引
        }
    }
}
