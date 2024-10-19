using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR;

public class VRWeaponController : MonoBehaviour
{
    public Gun gun;  // 引用枪支

    public SteamVR_Action_Boolean triggerAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");

    void Update()
    {
        // 检测手柄触发器输入
        if (triggerAction.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            if (gun != null)
            {
                gun.Shoot();  // 调用枪支的发射方法
                Debug.Log("Trigger pressed, Shoot() called.");
            }
        }
    }
}
