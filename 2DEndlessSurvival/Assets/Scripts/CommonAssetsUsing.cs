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

    public Transform pfSkeletonArrow;

    public Transform pfItemWorld;

    public Sprite weaponSprite;
    public Sprite healthPotionSprite;
    public Sprite coinSprite;
    public Sprite[] monsterPartSprite;
    public Sprite[] autoDrop;
}
