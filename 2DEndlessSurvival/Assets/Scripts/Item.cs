using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Item
{
    public static Item CreateItem(ItemType itemType, int amount)
    {
        Item item = new Item { itemType = itemType, amount = amount };
        return item;
    }
    public enum ItemType
    {
        Weapon,
        HealthPotion,
        ManaPotion,
        CopperCoin,
        SilverCoin,
        GoldenCoin,
        Bone,
        Skull,
        MonsterEye,
        Feather,
        Fabric,
        Empty,
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch(itemType)
        {
            default:
            case ItemType.Weapon: return CommonAssetsUsing.i.weaponSprite;
            case ItemType.HealthPotion: return CommonAssetsUsing.i.healthPotionSprite;
            case ItemType.ManaPotion: return CommonAssetsUsing.i.manaPotionSprite;
            case ItemType.CopperCoin: return CommonAssetsUsing.i.coinSprite[0];
            case ItemType.SilverCoin: return CommonAssetsUsing.i.coinSprite[1];
            case ItemType.GoldenCoin: return CommonAssetsUsing.i.coinSprite[2];
            case ItemType.Empty: return CommonAssetsUsing.i.emptySprite;
            case ItemType.Bone: return CommonAssetsUsing.i.monsterPartSprite[0];
            case ItemType.Skull: return CommonAssetsUsing.i.monsterPartSprite[1];
            case ItemType.MonsterEye: return CommonAssetsUsing.i.monsterPartSprite[2];
            case ItemType.Feather: return CommonAssetsUsing.i.monsterPartSprite[3];
            case ItemType.Fabric: return CommonAssetsUsing.i.monsterPartSprite[4];
        }
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Feather:
            case ItemType.MonsterEye:
            case ItemType.Skull:
            case ItemType.Bone:
            case ItemType.HealthPotion:
            case ItemType.ManaPotion:
            case ItemType.Fabric:
            case ItemType.CopperCoin:
            case ItemType.SilverCoin:
            case ItemType.GoldenCoin:
                return true;
            case ItemType.Weapon:
            case ItemType.Empty:
                return false;
        }
    }

    public int MaxStack()
    {
        switch (itemType)
        {
            default:
            case ItemType.Feather:
            case ItemType.MonsterEye:
            case ItemType.Skull:
            case ItemType.Bone:
            case ItemType.HealthPotion:
            case ItemType.ManaPotion:
            case ItemType.Fabric:
                return 5;
            case ItemType.Weapon:
            case ItemType.Empty:
                return 1;
        }
    }

}
