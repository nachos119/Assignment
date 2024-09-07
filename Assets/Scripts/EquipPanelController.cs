using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EquipPanelController : MonoBehaviour
{
    [SerializeField] private List<SlotController> equipList = new List<SlotController>((int)EEquip.End);

    private void Start()
    {
        PlayerManager.Instance.SetEquipReset = UpdateEquip;
    }

    public void Show()
    {
        UpdateEquip();

        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    private void UpdateEquip()
    {
        var equip = PlayerManager.Instance.SetEquipArray;
        var count = equip.Length;

        for (int i = 0; i < count; i++)
        {
            if (equip[i] != null)
            {
                equipList[i].UpdateItem(equip[i]);
            }
        }
    }
}
