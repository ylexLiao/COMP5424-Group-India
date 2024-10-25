using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR;

public class VRWeaponController : MonoBehaviour
{
    public SteamVR_Action_Boolean triggerAction; // �����ְ�������붯��
    public SteamVR_Input_Sources handType; // ѡ������
    public Gun gun; // �󶨵� Gun �ű�

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // ������ְ���Ƿ񱻰���
        if (triggerAction.GetStateDown(handType))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (gun != null)
        {
            gun.Shoot(); // ���� Gun �ű��е� Shoot ����
        }
        else
        {
            Debug.LogError("Gun �ű�δ�󶨣�");
        }
    }
}
