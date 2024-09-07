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
            // 상점과 인벤토리 켜주기
            playerManager.SetShowInvetoryCallBack?.Invoke(true);
            shopPanelController.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerManager.SetOtherAction = false;
            // 꺼주기
            playerManager.SetShowInvetoryCallBack?.Invoke(false);
            shopPanelController.gameObject.SetActive(false);
        }
    }
}
