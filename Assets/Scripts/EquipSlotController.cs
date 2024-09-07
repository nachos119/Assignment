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
                // �ش� ������Ʈ�� ���̾ dropLayerMask�� ���� �ִ��� üũ
                Debug.Log("�巡�� ���� ������ ������Ʈ�� ������ ���̾ ���� �ֽ��ϴ�: " + hoveredObject.name);
                Debug.Log(hoveredObject.layer);
                Debug.Log($"{hoveredObject.layer}");
            }
            else
            {
                Debug.Log("������ ���̾ ������ �ʴ� ������Ʈ�Դϴ�.");
            }
        }
    }
}