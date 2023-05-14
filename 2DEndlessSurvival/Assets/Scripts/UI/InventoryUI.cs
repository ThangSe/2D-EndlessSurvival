using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CodeMonkey.Utils;
using System.Linq;

public class InventoryUI : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    private Player player;

    private void Awake()
    {
        itemSlotContainer = transform.Find("ItemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("ItemSlotTemplate");
        Player.Instance.OnUseItem += Player_OnUseItem;
    }

    private void Player_OnUseItem(object sender, Player.OnUseItemEventArgs e)
    {
        if (e.slot > inventory.GetItemList().Count) return;
        int index = e.slot - 1;
        Item itemUsed = inventory.GetItem(index);
        inventory.UseItem(itemUsed);
        itemUsed.amount--;
        if (itemUsed.amount == 0) inventory.RemoveItem(index);
        RefreshInventoryItems();
    }

    public void SetInventory (Inventory inventory)
    {
        this.inventory = inventory;
        inventory.OnItemListChanged += Inventory_OnItemListChanged;
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        int slot = 0;
        float itemSlotCellSize = 100f;
        if(slot < 4)
        {
            foreach (var x in inventory.GetItemList().Select((item, index) => (item, index)))
            {
                RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
                itemSlotRectTransform.gameObject.SetActive(true);
                itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () => {
                    //Use Item
                    inventory.UseItem(x.item);
                    x.item.amount--;
                    if(x.item.amount == 0) inventory.RemoveItem(x.index);
                    RefreshInventoryItems();
                };
                itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () => {
                    //Drop Item
                    inventory.RemoveItem(x.index);
                    ItemWorld.DropItem(player.transform.GetChild(0).Find("DropPosition").position, x.item);
                };
                itemSlotRectTransform.anchoredPosition = new Vector2(slot * itemSlotCellSize, 0);
                Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
                image.sprite = x.item.GetSprite();
                var tempColor = image.color;
                tempColor.a = 1f;
                image.color = tempColor;
                TextMeshProUGUI text = itemSlotRectTransform.Find("AmountText").GetComponent<TextMeshProUGUI>();
                if (x.item.amount > 1)
                {
                    text.SetText(x.item.amount.ToString());
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
