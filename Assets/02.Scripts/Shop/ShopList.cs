using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopList : ScriptableObject
{
    public List<ShopItemSO> itemList = new List<ShopItemSO>();

    public void AdditemList(ShopItemSO _shopitem)
    {
        itemList.Add(_shopitem);
    }

    public void RemoveitemList(ShopItemSO _shopitem)
    {
        if (itemList.Contains(_shopitem))
        {
            itemList.Remove(_shopitem);
        }
    }
}
