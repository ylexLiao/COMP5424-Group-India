using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR;

public class VRWeaponController : MonoBehaviour
{
    public SteamVR_Action_Boolean triggerAction; // 绑定右手扳机的输入动作
    public SteamVR_Input_Sources handType; // 选择右手
    public Gun gun; // 绑定的 Gun 脚本

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 检查右手扳机是否被按下
        if (triggerAction.GetStateDown(handType))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (gun != null)
        {
            gun.Shoot(); // 调用 Gun 脚本中的 Shoot 方法
        }
        else
        {
            Debug.LogError("Gun 脚本未绑定！");
        }
    }
}
