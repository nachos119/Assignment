using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PreviewPanelController : MonoBehaviour
{
    [SerializeField] private RectTransform previewObj = null;
    [SerializeField] private TMP_Text previewTitleText = null;
    [SerializeField] private TMP_Text previewExplanationText = null;
    [SerializeField] private Image previewImage = null;

    [SerializeField] private RectTransform dragObj = null;
    [SerializeField] private Image dragImage = null;

    private PreviewPanelManager previewPanelManager = null;

    private void Awake()
    {
        previewPanelManager = PreviewPanelManager.Instance;
    }

    private void LateUpdate()
    {
        if (previewPanelManager.SetOnPreviewPanel)
        {
            previewObj.position = Input.mousePosition;
        }

        if (previewPanelManager.SetIsDrag)
        {
            dragObj.position = Input.mousePosition;
        }

        ActivePreviewObj();
        ActiveDargObj();
    }

    private void ActivePreviewObj()
    {
        if (previewPanelManager.SetPreviewItem != null)
        {
            previewTitleText.text = previewPanelManager.SetPreviewItem.itemName;
            previewExplanationText.text = previewPanelManager.SetPreviewItem.itemName;
            previewImage.sprite = previewPanelManager.SetPreviewItem.image;
        }

        previewObj.gameObject.SetActive(previewPanelManager.SetOnPreviewPanel);
    }

    private void ActiveDargObj()
    {
        if (previewPanelManager.SetPreviewItem != null)
        {
            dragImage.sprite = previewPanelManager.SetPreviewItem.image;
        }

        dragObj.gameObject.SetActive(previewPanelManager.SetIsDrag);
    }
}
