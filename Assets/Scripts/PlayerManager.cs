using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : LazySingleton<PlayerManager>
{
    private Item[] equipArray = new Item[(int)EEquip.End];

    private List<Item> inventory = new List<Item>();
    private string name;
    private bool isOtherAction = false;

    private Action updateNickNameCallBack = null;
    private Action<bool> showInvetoryCallBack = null;

    private Action inventoryReset = null;
    private Action equipReset = null;

    private Action<int, string> updateParts = null;

    public string SetNickName { get { return name; } set { name = value; } }
    public bool SetOtherAction { get { return isOtherAction; } set { isOtherAction = value; } }
    public Action SetUpdateNickName { get { return updateNickNameCallBack; } set { updateNickNameCallBack = value; } }
    public Action<bool> SetShowInvetoryCallBack { get { return showInvetoryCallBack; } set { showInvetoryCallBack = value; } }

    public Item[] SetEquipArray { get { return equipArray; } set { equipArray = value; } }
    public Action SetInventoryReset { get { return inventoryReset; } set { inventoryReset = value; } }
    public Action SetEquipReset { get { return equipReset; } set { equipReset = value; } }
    public Action<int, string> SetUpdateParts { get { return updateParts; } set { updateParts = value; } }

    public void BuyInventory(Item _item)
    {
        inventory.Add(_item);

        inventoryReset?.Invoke();
    }

    public List<Item> GetInventory()
    {
        return inventory;
    }

    public void SellInventory(Item _item)
    {
        inventory.Remove(_item);

        inventoryReset?.Invoke();
    }

    public void UpdateEquip(Item _item)
    {
        var beforeItem = equipArray[(int)_item.type];
        equipArray[(int)_item.type] = _item;

        var equipData = inventory.Find(data => data.itemName == _item.itemName);
        equipData = beforeItem;

        updateParts?.Invoke((int)_item.type, _item.itemName);
        inventoryReset?.Invoke();
        equipReset?.Invoke();
    }
}
