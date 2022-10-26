using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Products", order = 2)]
public class ProductsScriptableObject : ScriptableObject
{
    public int[] reditems = new int[3];
    public int[] greenitems = new int[3];
    public int[] purpleitems = new int[3];
    public int[] yellowitems = new int[3];
    public int[] cyanitems = new int[3];
    public int[] blueitems = new int[3];
    private int[][] items = new int[6][];

    public void SetItems()
    {
        items[0] = reditems;
        items[1] = greenitems;
        items[2] = purpleitems;
        items[3] = yellowitems;
        items[4] = cyanitems;
        items[5] = blueitems;
    }

    public void AddItems(int numberOfItems, int colorIndex, int rarityIndex)
    {
        if (items[colorIndex][rarityIndex] < 1000000)
        {
            items[colorIndex][rarityIndex] += numberOfItems;
        }
        else
        {
            Debug.LogError("Can not have any more of item: " + rarityIndex);
        }
    }

    public void SubtractItems(int numberOfItems, int colorIndex, int rarityIndex)
    {
        if (items[colorIndex][rarityIndex] < 1)
        {
            items[colorIndex][rarityIndex] -= numberOfItems;
        }
        else
        {
            Debug.LogError("Can not use any more of item: " + rarityIndex);
        }
    }

    public int GetItemCount(int colorIndex, int rarityIndex)
    {
        return items[colorIndex][rarityIndex];
    }
}
