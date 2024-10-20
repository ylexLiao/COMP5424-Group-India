using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR;

public class VRWeaponController : MonoBehaviour
{
    public WeaponBase weapon;  // 你绑定的武器

    // 定义SteamVR的输入动作，默认为右手触发器
    public SteamVR_Action_Boolean triggerAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");

    void Update()
    {
        // 检测右手触发器输入，SteamVR_Input_Sources.RightHand 表示右手
        if (triggerAction.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            if (weapon != null)
            {
                weapon.Attack();  // 调用武器的攻击方法
            }
        }
    }
}
