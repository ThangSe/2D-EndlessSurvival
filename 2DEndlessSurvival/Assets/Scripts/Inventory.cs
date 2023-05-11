using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Inventory
{
    public event EventHandler OnItemListChanged;
    private List<Item> itemList;
    private int maxSlot = 5;
    private int emptySlot;

    public Inventory()
    {
        itemList = new List<Item>();
        emptySlot = maxSlot;
    }

    public int AddItem(Item item)
    {
        int amountItemAfterPickedUp = 0;
        if(emptySlot > 0)
        {
            if (item.IsStackable())
            {
                bool itemNotFullStackInInventory = false;
                int amountNewItemInc = item.amount;
                foreach (Item inventoryItem in itemList)
                {
                    if (inventoryItem.itemType == item.itemType)
                    {                        
                        int tryAddAmount = inventoryItem.amount + item.amount;
                        if(tryAddAmount > item.maxStack())
                        {
                            inventoryItem.amount = item.maxStack();
                            amountNewItemInc = tryAddAmount - item.maxStack();
                            itemNotFullStackInInventory = false;
                        } else
                        {
                            inventoryItem.amount += item.amount;
                            amountNewItemInc = 0;
                            itemNotFullStackInInventory = true;
                        }
                    }
                }
                if (!itemNotFullStackInInventory)
                {
                    item.amount = amountNewItemInc;
                    itemList.Add(item);
                    emptySlot--;                  
                }
                OnItemListChanged?.Invoke(this, EventArgs.Empty);
                return amountItemAfterPickedUp;
            }
            else
            {
                itemList.Add(item);
                emptySlot--;
                OnItemListChanged?.Invoke(this, EventArgs.Empty);
                return amountItemAfterPickedUp;
            }
        } else
        {
            if (item.IsStackable())
            {
                if(itemList.Last().itemType == item.itemType)               
                {
                    int tryAddAmount = itemList.Last().amount + item.amount;
                    if (tryAddAmount > item.maxStack())
                    {
                        itemList.Last().amount = item.maxStack();
                        item.amount = tryAddAmount - item.maxStack();
                        OnItemListChanged?.Invoke(this, EventArgs.Empty);
                        return tryAddAmount - item.maxStack();
                    }
                    else
                    {
                        itemList.Last().amount += item.amount;
                        OnItemListChanged?.Invoke(this, EventArgs.Empty);
                        return amountItemAfterPickedUp;
                    }
                }
                return item.amount;
            } else
            {
                return item.amount;
            }                
        }
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
