using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanelController : MonoBehaviour
{
    [SerializeField] private InventorySlotController slotController = null;
    [SerializeField] private GameObject contentObject = null;

    private readonly int inventoryMaxCount = 24;

    private List<InventorySlotController> inventorySlotList = null;

    private void Awake()
    {
        SetData();
    }

    public void Show()
    {
        Cursor.visible = true;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        Cursor.visible = false;
        gameObject.SetActive(false);
    }

    private void SetData()
    {
        inventorySlotList = new List<InventorySlotController>(inventoryMaxCount);

        for (int i = 0; i < inventoryMaxCount; i++)
        {
            var slot = Instantiate(slotController, contentObject.transform);
            slot.SetCallBack(SellItem);
            inventorySlotList.Add(slot);
        }

        PlayerManager.Instance.SetInventoryReset = UpdateInventory;
    }

    private void SellItem(Item _itme)
    {
        UpdateInventory();
    }

    private void UpdateInventory()
    {
        ResetInventory();

        var data = PlayerManager.Instance.GetInventory();
        var count = data.Count;
        for (int i = 0; i < count; i++)
        {
            inventorySlotList[i].UpdateItem(data[i]);
        }
    }

    private void ResetInventory()
    {
        var count = inventorySlotList.Count;
        for (int i = 0; i < count; i++)
        {
            inventorySlotList[i].UpdateItem(null);
        }
    }
}
