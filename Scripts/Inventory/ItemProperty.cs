using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemProperty
{
    public enum ItemType
    {
        weapon,
        helm,
        chest,
        pants,
        gloves,
        boots,
        item,
        potion
    }
    public ItemType itemtype;
    public string itemName;
    public Sprite itemImage;
    public int itemCost;
    public float itemAttackDamage;
    public int itemHp;
    public int itemCount = 1;
}
