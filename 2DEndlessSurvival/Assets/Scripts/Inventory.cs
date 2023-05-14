using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Inventory
{
    public event EventHandler OnItemListChanged;
    private List<Item> itemList;
    private Action<Item> useItemAction;
    private int maxSlot = 5;
    private int emptySlot;

    public Inventory(Action<Item> useItemAction)
    {
        this.useItemAction = useItemAction;
        itemList = new List<Item>();
        emptySlot = maxSlot;
    }

    public int AddItem(Item item)
    {
        int amountItemAfterPickedUp = 0;
        if (item.itemType == Item.ItemType.CopperCoin || item.itemType == Item.ItemType.SilverCoin || item.itemType == Item.ItemType.GoldenCoin) return item.amount;
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
                        if(tryAddAmount > item.MaxStack())
                        {
                            inventoryItem.amount = item.MaxStack();
                            amountNewItemInc = tryAddAmount - item.MaxStack();
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
                foreach(Item itemInvetory in itemList)
                {
                    if(itemInvetory.itemType == item.itemType)
                    {
                        if(itemInvetory.amount < itemInvetory.MaxStack())
                        {
                            int tryAddAmount = itemInvetory.amount + item.amount;
                            if (tryAddAmount > itemInvetory.MaxStack())
                            {
                                itemInvetory.amount = itemInvetory.MaxStack();
                                item.amount = tryAddAmount - itemInvetory.amount;
                                OnItemListChanged?.Invoke(this, EventArgs.Empty);
                            }
                            else
                            {
                                itemInvetory.amount += item.amount;
                                item.amount -= item.amount;
                                OnItemListChanged?.Invoke(this, EventArgs.Empty);
                            }
                        }                    
                    }
                }              
            }
            return item.amount;
        }
    }

    public void RemoveItem(int index)
    {
        itemList.RemoveAt(index);
        emptySlot++;
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }

    public void UseItem(Item item)
    {
        useItemAction(item);
    }
    public Item GetItem(int index)
    {
        return itemList[index];
    }
}
