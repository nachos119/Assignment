using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanelController : MonoBehaviour
{
    [SerializeField] private ShopSlotController slotController = null;
    [SerializeField] private GameObject contentObject = null;

    private readonly int shopMaxCount = 16;

    private List<ShopSlotController> shopList = null;
    private ItemManager itemManager = null;

    private List<Item> itemList = null;

    private void Awake()
    {
        itemManager = ItemManager.Instance;

        SetData();
    }

    private void SetData()
    {
        shopList = new List<ShopSlotController>(shopMaxCount);

        for (int i = 0; i < shopMaxCount; i++)
        {
            var slot = Instantiate(slotController, contentObject.transform);
            slot.SetCallBack(BuyItem);
            shopList.Add(slot);
        }

        itemList = itemManager.GetShopList();

        var count = itemList.Count;
        for (int i = 0; i < count; i++)
        {
            shopList[i].UpdateItem(itemList[i]);
        }

        itemManager.SetShopItemUpdateCallback = AddShopItem;
    }

    private void BuyItem(Item _itme)
    {
        itemList.Remove(_itme);
        UpdateShop();
    }

    private void UpdateShop()
    {
        ResetShop();

        var count = itemList.Count;
        for (int i = 0; i < count; i++)
        {
            shopList[i].UpdateItem(itemList[i]);
        }
    }

    private void ResetShop()
    {
        var count = shopList.Count;
        for (int i = 0; i < count; i++)
        {
            shopList[i].UpdateItem(null);
        }
    }

    private void AddShopItem(Item _item)
    {
        itemList.Add(_item);

        UpdateShop();
    }
}
