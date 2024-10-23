using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Health Potion", menuName = "Items/Health Potion")]
public class HealthupPickup : Item
{
    // Start is called before the first frame update
    public int healAmount = 20;

    public override void Use()
    {
        // ʵ�ָֻ�����ֵ��Ч��
        Debug.Log("ʹ��������ҩˮ���ָ��� " + healAmount + " ������ֵ��");


        // ������һ�� Player �࣬����һ�� IncreaseHealth ����
        HealthController player = FindObjectOfType<HealthController>();
        if (player != null)
        {
            player.IncreaseHealth(healAmount);
        }

        // ���ٵ���
   
    }
    // ��������Ӿ�����߼�������������ҵ�����ֵ


    // ������һ�� Player �࣬����һ�� IncreaseHealth ����
    // Player.Instance.IncreaseHealth(healAmount);
}
