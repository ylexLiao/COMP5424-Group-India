using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemStatusUI : MonoBehaviour
{
    public Image itemImage;

    public enum ItemType
    {
        None,
        HealthupPickup,
        SlowdownPickup,
        AreaDamageItem
    }

    private ItemType currentItem = ItemType.None;

    void Start()
    {
        UpdateItemUI();
    }

    public void SetItem(ItemType itemType)
    {
        currentItem = itemType;
        UpdateItemUI();
    }

    private void UpdateItemUI()
    {
        switch (currentItem)
        {
            case ItemType.None:
                itemImage.color = Color.gray;
                break;
            case ItemType.HealthupPickup:
                itemImage.color = Color.red;
                break;
            case ItemType.SlowdownPickup:
                itemImage.color = Color.blue;
                break;
            case ItemType.AreaDamageItem:
                itemImage.color = new Color(1.0f, 0.65f, 0.0f); // ³ÈÉ«
                break;
        }
    }
}