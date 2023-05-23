using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonAssetsUsing : MonoBehaviour
{
    public static CommonAssetsUsing _i;

    public static CommonAssetsUsing i
    {
        get
        {
            if (_i == null) _i = Instantiate(Resources.Load<CommonAssetsUsing>("CommonAssetsUsing"));
            return _i;
        }
        
    }

    public Transform pfDamagePopup;
    public Transform pfPlayerPopup;

    public Transform pfSkeletonArrow;

    public Transform pfItemWorld;

    public Sprite weaponSprite;
    public Sprite healthPotionSprite;
    public Sprite manaPotionSprite;
    public Sprite[] coinSprite;
    public Sprite emptySprite;
    public Sprite[] monsterPartSprite;

    public float commonDropRate = .9f;
    public float alwaysDropRate = 1f;
}
