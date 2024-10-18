using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR;

public class VRWeaponController : MonoBehaviour
{
    public WeaponBase weapon;  // ��󶨵�����

    // ����SteamVR�����붯����Ĭ��Ϊ���ִ�����
    public SteamVR_Action_Boolean triggerAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");

    void Update()
    {
        // ������ִ��������룬SteamVR_Input_Sources.RightHand ��ʾ����
        if (triggerAction.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            if (weapon != null)
            {
                weapon.Attack();  // ���������Ĺ�������
            }
        }
    }
}
