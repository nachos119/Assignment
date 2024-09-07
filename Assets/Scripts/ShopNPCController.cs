using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNPCController : MonoBehaviour
{
    [SerializeField] private ShopPanelController shopPanelController = null;

    private PlayerManager playerManager = null;

    private void Awake()
    {
        playerManager = PlayerManager.Instance;
    }

    private void Start()
    {
        shopPanelController = Instantiate(shopPanelController);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerManager.SetOtherAction = true;

            playerManager.SetShowInvetoryCallBack?.Invoke(true);
            shopPanelController.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerManager.SetOtherAction = false;

            playerManager.SetShowInvetoryCallBack?.Invoke(false);
            shopPanelController.gameObject.SetActive(false);

            PreviewPanelManager.Instance.SetOnPreviewPanel = false;
            PreviewPanelManager.Instance.SetIsDrag = false;
        }
    }
}
