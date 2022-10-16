using System;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    public ItemData itemData;

    public int amount = 1;

    public static Action<ItemData, int> OnAddItem;

    public void AddItem()
    {
        OnAddItem?.Invoke(itemData,amount);
    }
}
