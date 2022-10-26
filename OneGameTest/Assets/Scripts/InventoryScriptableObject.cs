using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Inventory", order = 1)]
public class InventoryScriptableObject : ScriptableObject
{
    public int[] items = new int[6];

    public void AddItems(int numberOfItems, int itemIndex)
    {
        if (items[itemIndex] < 1000000)
        {
            items[itemIndex] += numberOfItems;
        }
        else
        {
            Debug.LogError("Can not have any more of item: " + itemIndex);
        }
    }

    public void SubtractItems(int numberOfItems, int itemIndex)
    {
        if (items[itemIndex] < 1)
        {
            items[itemIndex] -= numberOfItems;
        }
        else
        {
            Debug.LogError("Can not use any more of item: " + itemIndex);
        }
    }

    public int GetItemCount(int itemIndex)
    {
        return items[itemIndex];
    }
}
