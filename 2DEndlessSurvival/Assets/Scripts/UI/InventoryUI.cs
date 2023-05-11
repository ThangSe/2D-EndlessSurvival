using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;

    private void Awake()
    {
        itemSlotContainer = transform.Find("ItemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("ItemSlotTemplate");
    }

    public void SetInventory (Inventory inventory)
    {
        this.inventory = inventory;
        inventory.OnItemListChanged += Inventory_OnItemListChanged;
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        /*foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }*/
        int slot = 0;
        float itemSlotCellSize = 100f;
        if(slot < 4)
        {
            foreach (Item item in inventory.GetItemList())
            {
                //RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
                RectTransform itemSlotRectTransform = itemSlotContainer.GetChild(slot).GetComponent<RectTransform>();
                //itemSlotRectTransform.gameObject.SetActive(true);
                itemSlotRectTransform.anchoredPosition = new Vector2(slot * itemSlotCellSize, 0);
                Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
                image.sprite = item.GetSprite();
                var tempColor = image.color;
                tempColor.a = 1f;
                image.color = tempColor;
                TextMeshProUGUI text = itemSlotRectTransform.Find("AmountText").GetComponent<TextMeshProUGUI>();
                if (item.amount > 1)
                {
                    text.SetText(item.amount.ToString());
                }
                else
                {
                    text.SetText("");
                }
                slot++;
            }
        }       
    }
}
