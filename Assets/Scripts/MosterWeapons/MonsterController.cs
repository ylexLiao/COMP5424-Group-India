using UnityEngine;
using Valve.VR;
using YourProjectNamespace;

public class MonsterController : MonoBehaviour
{
    public GameObject weaponPrefab; // С�ֵ�����Ԥ����

    private GameObject weapon;
    private MonsterWeaponController weaponController;

    private void Start()
    {
        // ʵ��������
        weapon = Instantiate(weaponPrefab, transform.position, Quaternion.identity);

        // ��ȡС�ֵ�����λ�ã�����С����һ����Ϊ "Hand_R" ���������ʾ���֣�
        Transform rightHand = transform.Find("Hand_R");
        if (rightHand != null)
        {
            // ��ȡ SteamVR �����λ�ú���ת
            SteamVR_Camera steamVRCamera = FindObjectOfType<SteamVR_Camera>();
            if (steamVRCamera != null)
            {
                Vector3 cameraPosition = steamVRCamera.transform.position;
                Quaternion cameraRotation = steamVRCamera.transform.rotation;

                // ��������λ�ú���ת����Ϊ����� SteamVR �����λ��
                weapon.transform.SetParent(rightHand);
                weapon.transform.localPosition = Vector3.zero;
                weapon.transform.localRotation = Quaternion.identity;
                weapon.transform.position = cameraPosition + rightHand.position;
                weapon.transform.rotation = cameraRotation * rightHand.rotation;
            }
        }

        // ��ȡ�����ϵĿ��������
        weaponController = weapon.GetComponent<MonsterWeaponController>();
    }

    private void Update()
    {
        // ���������һ���ض��ı�ǩ "Player"
        Collider[] playersInRange = Physics.OverlapSphere(transform.position, weaponController.detectionRange, LayerMask.GetMask("Player"));
        if (playersInRange.Length > 0)
        {
            weaponController.Update();
        }
    }

    public void OnDefeated()
    {
        if (weapon != null)
        {
            Destroy(weapon);
        }
    }
}