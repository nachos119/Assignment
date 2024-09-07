using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopSlotController : SlotController
{
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

            var hoveredObject = hovered.Find(data => data.layer == LayerMask.NameToLayer("Inventory"));
            if (hoveredObject != null)
            {
                var buyItem = item;
                PlayerManager.Instance.BuyInventory(buyItem);
                callBack?.Invoke(buyItem);
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

        var buyItem = item;
        PlayerManager.Instance.BuyInventory(buyItem);
        callBack?.Invoke(buyItem);
    }
}
