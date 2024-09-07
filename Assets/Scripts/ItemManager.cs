using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : LazySingleton<ItemManager>
{
    public List<Item> items = new List<Item>();

    private PlayerManager playerManager = null;

    private Action<Item> shopItemUpdateCallback = null;

    public Action<Item> SetShopItemUpdateCallback { get { return shopItemUpdateCallback; } set { shopItemUpdateCallback = value; } }

    public Item GetItemByName(string name)
    {
        return items.Find(item => item.itemName == name);
    }

    public void SetData()
    {
        playerManager = PlayerManager.Instance;

        items.Add(new Item("Hammer01", EItemType.Weapon));
        items.Add(new Item("Sword01", EItemType.Weapon));
        items.Add(new Item("Sword02", EItemType.Weapon));
        items.Add(new Item("Sword03", EItemType.Weapon));
        items.Add(new Item("Sword04", EItemType.Weapon));
        items.Add(new Item("Wand01", EItemType.Weapon));
        items.Add(new Item("Wand02", EItemType.Weapon));

        items.Add(new Item("Cloth1", EItemType.Armor));
        items.Add(new Item("Cloth2", EItemType.Armor));
        items.Add(new Item("Cloth3", EItemType.Armor));
        items.Add(new Item("Cloth4", EItemType.Armor));

        playerManager.SetEquipArray[(int)EEquip.RWeapon] = new Item("Sword01", EItemType.Weapon);
        playerManager.SetEquipArray[(int)EEquip.Armor] = new Item("Cloth1", EItemType.Armor);
    }

    public List<Item> GetShopList()
    {
        var data = items;

        var count = playerManager.SetEquipArray.Length;
        for (int i = 0; i < count; i++)
        {
            if (playerManager.SetEquipArray[i] != null)
            {
                var findData = data.Find(data => data.itemName == playerManager.SetEquipArray[i].itemName);
                data.Remove(findData);
            }
        }

        return data;
    }
}
