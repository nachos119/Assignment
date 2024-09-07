using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPlayer_State
{
    Idle = 0,
    Walk = 1,
    Attack = 2,
    Damage = 3,
    Die = 4,
    End = 5
}

public enum EEquip
{
    Hat = 0,
    Shoulder = 1,
    Armor = 2,
    Belt = 3,
    Gloves = 4,
    Shoes = 5,
    LWeapon = 6,
    RWeapon = 7,
    End = 8
}

public enum EItemType
{
    Hat = 0,
    Shoulder = 1,
    Armor = 2,
    Belt = 3,
    Gloves = 4,
    Shoes = 5,
    Weapon = 7,
    End = 8
}

public class Item
{
    public string itemName;
    public EItemType type;
    public Sprite image;

    public Item(string _name, EItemType _type)
    {
        itemName = _name;
        type = _type;

        Sprite loadedSprite = Resources.Load<Sprite>("ItemImage/" + _name);

        if (loadedSprite != null)
        {
            image = loadedSprite;
        }
    }
}