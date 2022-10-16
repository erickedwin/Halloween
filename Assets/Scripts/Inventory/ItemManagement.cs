using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemManagement : MonoBehaviour
{
    public readonly Dictionary<ItemData, Item> inventory = new Dictionary<ItemData, Item>();

    public static Action OnAdded;

    public static Action OnRemoved;

    public static Action OnInventoryChange;

    public static Action<ItemData> OnModifiedItem;

    public void Add(ItemData item, int amount = 1)
    {
        if (inventory.TryGetValue(item, out var data))
        {
            var newItem = new Item(amount, item);
            inventory.Add(item, newItem);
        }
        else
        {
            data.amount += amount;
        }
    }

    public void Remove(ItemData item, int amount = 1)
    {
        if (inventory.TryGetValue(item, out var data))
        {
            if (data.amount < amount) return;
            data.amount -= amount;
            if (data.amount <= 0)
            {
                inventory.Remove(item);
            }
        }
        else
        {
            return;
        }
    }

    public Item GetItem(ItemData key)
    {
        return inventory.TryGetValue(key, out var result) ? result : null;
    }

    public bool TryGetItem(ItemData key, out Item result)
    {
        return inventory.TryGetValue(key, out result);
    }

    public void CheckSafe()
    {
    }

    public void ExitSafe()
    {
    }

    private void OnEnable()
    {
        ItemContainer.OnAddItem += Add;
    }

    private void OnDisable()
    {
        ItemContainer.OnAddItem -= Add;
    }
}

[Serializable]
public class Item
{
    public int amount;

    public ItemData data;

    public Item(int amount, ItemData data)
    {
        this.amount = amount;
        this.data = data;
    }
}