using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR;

public class VRWeaponController : MonoBehaviour
{
    public Gun gun;  // ����ǹ֧

    public SteamVR_Action_Boolean triggerAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");

    void Update()
    {
        // ����ֱ�����������
        if (triggerAction.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            if (gun != null)
            {
                gun.Shoot();  // ����ǹ֧�ķ��䷽��
                Debug.Log("Trigger pressed, Shoot() called.");
            }
        }
    }
}
