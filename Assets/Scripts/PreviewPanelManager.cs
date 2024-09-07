using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewPanelManager : LazySingleton<PreviewPanelManager>
{
    private bool onPreviewPanel = false;
    private bool isDrag = false;
    private Item previewItem = null;

    public bool SetOnPreviewPanel { get { return onPreviewPanel; } set { onPreviewPanel = value; } }
    public bool SetIsDrag { get { return isDrag; } set { isDrag = value; } }
    public Item SetPreviewItem { get { return previewItem; } set { previewItem = value; } }
}
