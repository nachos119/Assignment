using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneController : MonoBehaviour
{
    [SerializeField] private NickNameSettingPanelController nickNameSettingPanelController = null;
    [SerializeField] private InventoryPanelController inventoryPanelController = null;
    [SerializeField] private EquipPanelController equipPanelController = null;
    [SerializeField] private PreviewPanelController previewPanelController = null;

    [SerializeField] private NickNamePanelController nickNamePanelController = null;
    [SerializeField] private GameObject playerObject = null;

    private PlayerManager playerManager = null;
    private ItemManager itemManager = null;

    private void Awake()
    {
        playerManager = PlayerManager.Instance;
        itemManager = ItemManager.Instance;

        playerManager.SetUpdateNickName = UpdateNickName;
        playerManager.SetShowInvetoryCallBack = ShowInvetory;
    }

    private void Start()
    {
        nickNameSettingPanelController = Instantiate(nickNameSettingPanelController);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!playerManager.SetOtherAction)
            {
                if (equipPanelController.gameObject.activeSelf)
                    equipPanelController.Hide();
                else
                    equipPanelController.Show();
            }
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            if (!playerManager.SetOtherAction)
            {
                if (inventoryPanelController.gameObject.activeSelf)
                {
                    inventoryPanelController.Hide();
                }
                else
                {
                    inventoryPanelController.Show();
                }
            }
        }
    }

    private void SetData()
    {
        inventoryPanelController = Instantiate(inventoryPanelController);
        equipPanelController = Instantiate(equipPanelController);
        previewPanelController = Instantiate(previewPanelController);

        itemManager.SetData();

        Cursor.visible = false;

        Cursor.lockState = CursorLockMode.Confined;

    }

    private void UpdateNickName()
    {
        nickNamePanelController = Instantiate(nickNamePanelController, playerObject.transform);

        nickNamePanelController.UpdateNickName(playerManager.SetNickName);

        nickNamePanelController.gameObject.SetActive(true);

        SetData();
    }

    private void ShowInvetory(bool _isActive)
    {
        if(_isActive)
        {
            inventoryPanelController.Show();
        }
        else
        {
            inventoryPanelController.Hide();
        }
    }
}
