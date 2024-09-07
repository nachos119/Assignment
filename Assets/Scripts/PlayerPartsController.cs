using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPartsController : MonoBehaviour
{
    [SerializeField] private List<GameObject> armor = new List<GameObject>();
    [SerializeField] private List<GameObject> leftHand = new List<GameObject>();
    [SerializeField] private List<GameObject> rightHand = new List<GameObject>();

    private PlayerManager playerManager = null;

    private void Start()
    {
        playerManager = PlayerManager.Instance;

        playerManager.SetUpdateParts = SwitchParts;
    }

    public void SwitchParts(int _parts, string _partsName)
    {
        switch ((EEquip)_parts)
        {
            case EEquip.Hat:
                break;
            case EEquip.Shoulder:
                break;
            case EEquip.Armor:
                ResetParts(armor);
                UpdatePartsh(armor, _partsName);
                break;
            case EEquip.Belt:
                break;
            case EEquip.Gloves:
                break;
            case EEquip.Shoes:
                break;
            case EEquip.LWeapon:
                ResetParts(leftHand);
                UpdatePartsh(leftHand, _partsName);
                break;
            case EEquip.RWeapon:
                ResetParts(rightHand);
                UpdatePartsh(rightHand, _partsName);
                break;
            case EEquip.End:
                break;
        }
    }

    private void ResetParts(List<GameObject> _list)
    {
        var count = _list.Count;
        for (int i = 0; i < count; i++)
        {
            _list[i].SetActive(false);
        }
    }

    private void UpdatePartsh(List<GameObject> _list, string _partsName)
    {
        var result = _list.Find(parts => parts.name == _partsName);
        result?.SetActive(true);
    }
}
