using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Item
{
    public enum ItemType
    {
        Weapon,
        HealthPotion,
        Coin,
        Bone,
        Skull,
        MonsterEye,
        Feather,
        Fabric,
    }

    public ItemType itemType;
    public int amount;
    public int maxAmount;

    public Sprite GetSprite()
    {
        switch(itemType)
        {
            default:
            case ItemType.Weapon: return CommonAssetsUsing.i.weaponSprite;
            case ItemType.HealthPotion: return CommonAssetsUsing.i.healthPotionSprite;
            case ItemType.Coin: return CommonAssetsUsing.i.coinSprite;
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
            case ItemType.Coin:
            case ItemType.HealthPotion:
            case ItemType.Fabric:
                return true;
            case ItemType.Weapon:
                return false;
        }
    }

    public int maxStack()
    {
        switch (itemType)
        {
            default:
            case ItemType.Feather:
            case ItemType.MonsterEye:
            case ItemType.Skull:
            case ItemType.Bone:
            case ItemType.Coin:
            case ItemType.HealthPotion:
            case ItemType.Fabric:
                return 5;
            case ItemType.Weapon:
                return 1;
        }
    }
}
