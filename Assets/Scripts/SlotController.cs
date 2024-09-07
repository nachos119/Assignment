using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SlotController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] protected Image itemImage = null;

    protected Action<Item> callBack = null;
    protected Item item = null;

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button != 0)
        {
            return;
        }

        if (item != null)
        {
            PreviewPanelManager.Instance.SetPreviewItem = item;
            PreviewPanelManager.Instance.SetIsDrag = true;
            Debug.Log($"{item.itemName} 드래그 시작");
        }
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        if (eventData.button != 0)
        {
            return;
        }

        if (item != null)
        {
            PreviewPanelManager.Instance.SetPreviewItem = item;
            PreviewPanelManager.Instance.SetIsDrag = true;
            Debug.Log($"{item.itemName} 드래그 중");
        }
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button != 0)
        {
            return;
        }

        if (item != null)
        {
            PreviewPanelManager.Instance.SetPreviewItem = item;

            PreviewPanelManager.Instance.SetIsDrag = false;
            Debug.Log($"{item.itemName} 드래그 끝");
        }
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null && !PreviewPanelManager.Instance.SetIsDrag)
        {
            PreviewPanelManager.Instance.SetPreviewItem = item;
            PreviewPanelManager.Instance.SetOnPreviewPanel = true;
        }
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (item != null)
        {
            PreviewPanelManager.Instance.SetOnPreviewPanel = false;
            PreviewPanelManager.Instance.SetPreviewItem = null;
        }
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Right || PreviewPanelManager.Instance.SetIsDrag)
        {
            return;
        }
    }

    public void SetCallBack(Action<Item> _callBack)
    {
        callBack = _callBack;
    }

    public void UpdateItem(Item _item)
    {
        item = _item;
        itemImage.sprite = item?.image;

        UpdateColor();
    }

    private void UpdateColor()
    {
        if (item != null)
        {
            itemImage.color = Color.white;
        }
        else
        {
            itemImage.color = Color.clear;
        }
    }
}
