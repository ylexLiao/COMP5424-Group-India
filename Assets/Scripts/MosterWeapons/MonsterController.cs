using UnityEngine;
using Valve.VR;
using YourProjectNamespace;

public class MonsterController : MonoBehaviour
{
    public GameObject weaponPrefab; // 小怪的武器预制体

    private GameObject weapon;
    private MonsterWeaponController weaponController;

    private void Start()
    {
        // 实例化武器
        weapon = Instantiate(weaponPrefab, transform.position, Quaternion.identity);

        // 获取小怪的右手位置（假设小怪有一个名为 "Hand_R" 的子物体表示右手）
        Transform rightHand = transform.Find("Hand_R");
        if (rightHand != null)
        {
            // 获取 SteamVR 相机的位置和旋转
            SteamVR_Camera steamVRCamera = FindObjectOfType<SteamVR_Camera>();
            if (steamVRCamera != null)
            {
                Vector3 cameraPosition = steamVRCamera.transform.position;
                Quaternion cameraRotation = steamVRCamera.transform.rotation;

                // 将武器的位置和旋转设置为相对于 SteamVR 相机的位置
                weapon.transform.SetParent(rightHand);
                weapon.transform.localPosition = Vector3.zero;
                weapon.transform.localRotation = Quaternion.identity;
                weapon.transform.position = cameraPosition + rightHand.position;
                weapon.transform.rotation = cameraRotation * rightHand.rotation;
            }
        }

        // 获取武器上的控制器组件
        weaponController = weapon.GetComponent<MonsterWeaponController>();
    }

    private void Update()
    {
        // 假设玩家有一个特定的标签 "Player"
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