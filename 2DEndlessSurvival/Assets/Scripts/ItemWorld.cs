using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemWorld : MonoBehaviour
{
    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {   
        Transform transform = Instantiate(CommonAssetsUsing.i.pfItemWorld, position, Quaternion.identity);
        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);
        return itemWorld;
    }

    public static ItemWorld DropItem(Vector3 dropPosition,Item item)
    {
        ItemWorld itemWorld = SpawnItemWorld(dropPosition, item);
        return itemWorld;
    }

    public static void EnemyDrop(Vector3 dropPosition, Item item)
    {
        SpawnItemWorld(GetRandomDropPosition(dropPosition), item);
    }

    private static Vector3 GetRandomDropPosition(Vector3 dropPosition)
    {
        return dropPosition + GetRandomDir() * Random.Range(2f, 0);
    }
    public static Vector3 GetRandomDir()
    {
        return new Vector3(Random.Range(-1f, 1f), 0).normalized;
    }

    private Item item;
    private SpriteRenderer spriteRenderer;
    private TextMeshPro textMeshPro;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("AmountText").GetComponent<TextMeshPro>();
    }
    public void SetItem(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
        if(item.amount > 1)
        {
            textMeshPro.SetText(item.amount.ToString());
        } else
        {
            textMeshPro.SetText("");
        }
    }

    public void SetAmount(int amount)
    {
        textMeshPro.SetText(amount.ToString());
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
