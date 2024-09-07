using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotController : SlotController
{
    public int index;

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);

    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);

        if (item != null)
        {
            var hovered = eventData.hovered;

            if (PlayerManager.Instance.SetOtherAction)
            {
                var hoveredObject = hovered.Find(data => data.layer == LayerMask.NameToLayer("Shop"));

                if (hoveredObject != null)
                {
                    var sellItem = item;
                    PlayerManager.Instance.SellInventory(sellItem);
                    callBack?.Invoke(sellItem);
                    ItemManager.Instance.SetShopItemUpdateCallback?.Invoke(sellItem);
                }
            }
            else
            {
                var hoveredObject = hovered.Find(data => data.layer == LayerMask.NameToLayer("Equip"));

                if (hoveredObject != null)
                {
                    // ÀåÂø
                }
            }
        }
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);

        if (eventData.button != PointerEventData.InputButton.Right || PreviewPanelManager.Instance.SetIsDrag)
        {
            return;
        }

        if (PlayerManager.Instance.SetOtherAction)
        {
            var sellItem = item;
            PlayerManager.Instance.SellInventory(sellItem);
            callBack?.Invoke(sellItem);
            ItemManager.Instance.SetShopItemUpdateCallback?.Invoke(sellItem);
        }
        else
        {
            PlayerManager.Instance.UpdateEquip(item, index);
        }
    }
}