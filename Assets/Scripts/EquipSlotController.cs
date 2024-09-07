using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipSlotController : SlotController
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
                // 해당 오브젝트의 레이어가 dropLayerMask에 속해 있는지 체크
                Debug.Log("드래그 종료 지점의 오브젝트는 지정된 레이어에 속해 있습니다: " + hoveredObject.name);
                Debug.Log(hoveredObject.layer);
                Debug.Log($"{hoveredObject.layer}");
            }
            else
            {
                Debug.Log("지정된 레이어에 속하지 않는 오브젝트입니다.");
            }
        }
    }
}