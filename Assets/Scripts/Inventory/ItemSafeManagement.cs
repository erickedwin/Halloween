using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemSafeManagement : MonoBehaviour
{
    public readonly Dictionary<ItemData, Item> safe = new Dictionary<ItemData, Item>();

    public static Action OnAdded;

    public static Action OnRemoved;

    public static Action OnSafeChange;

    public void Add(ItemData item, int amount = 1)
    {
        if (safe.TryGetValue(item, out var data))
        {
            var newItem = new Item(amount, item);
            safe.Add(item, newItem);
        }
        else
        {
            data.amount += amount;
        }
    }

    public void Remove(ItemData item, int amount = 1)
    {
        if (safe.TryGetValue(item, out var data))
        {
            if (data.amount < amount) return;
            data.amount -= amount;
            if (data.amount <= 0)
            {
                safe.Remove(item);
            }
        }
        else
        {
            return;
        }
    }

    public Item GetItem(ItemData key)
    {
        return safe.TryGetValue(key, out var result) ? result : null;
    }

    public bool TryGetItem(ItemData key, out Item result)
    {
        return safe.TryGetValue(key, out result);
    }
}