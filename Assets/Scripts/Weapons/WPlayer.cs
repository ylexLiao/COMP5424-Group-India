using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPlayer : MonoBehaviour
{
    public WeaponBase currentWeapon; // 当前持有的武器

    private void Update()
    {
        // 按下鼠标左键执行攻击
        if (Input.GetMouseButtonDown(0) && currentWeapon != null)
        {
            currentWeapon.Attack();
        }

        // 按下1键切换为斧头
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipWeapon("Axe");
        }

        // 其他武器可以继续扩展，比如剑、弓箭等
    }

    // 装备武器
    public void EquipWeapon(string weaponName)
    {
        // 查找场景中所有的武器
        WeaponBase[] weapons = FindObjectsOfType<WeaponBase>();

        foreach (WeaponBase weapon in weapons)
        {
            if (weapon.weaponName == weaponName)
            {
                currentWeapon = weapon;
                Debug.Log("装备了武器：" + weaponName);
                break;
            }
        }

        if (currentWeapon == null)
        {
            Debug.LogWarning("未找到指定的武器：" + weaponName);
        }
    }
}
