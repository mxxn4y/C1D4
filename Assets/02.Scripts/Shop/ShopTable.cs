using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTable : MonoBehaviour
{

    private static ShopTable instance;
    public static ShopTable Instance
    {
        get
        {
            if (null == instance)
            {
                instance = new ShopTable();
            }
            return instance;
        }
    }


    public List<Dictionary<string, object>> shopCSV;

    public ShopTable()
    {
        shopCSV = CSVReader.Read("ShopCSV2");
    }

    public ShopItemData GetData(string itemName)
    {
        var itemData = new ShopItemData();

        foreach (var data in shopCSV)
        {
            if (itemName == data["itemName"].ToString())
            {
                itemData.itemName = data["itemName"].ToString();
                itemData.price = int.Parse(data["price"].ToString());
                itemData.itemType = StringToItem(data["itemType"].ToString());
                itemData.gemType = StringToGem(data["gemType"].ToString());
                itemData.maxDailyPurchase = int.Parse(data["maxDailyPurchase"].ToString());
                itemData.maxTotalPurchase = int.Parse(data["maxTotalPurchase"].ToString());
                itemData.isUnlimited = bool.Parse(data["isUnlimited"].ToString());

                return itemData;
            }
        }
        Debug.LogError($"ItemName가 존재하지 않습니다: {itemName}");
        return itemData;
    }

    /*여기 추후 방꾸미기할 때 필요 유무 결정될 듯 */
    private ItemType StringToItem(string itemType)
    {
        switch (itemType.ToLower())
        {
            case "wall": return ItemType.WALL;
            case "floor": return ItemType.FLOOR;
            case "slot": return ItemType.SLOT;
            case "ceiling": return ItemType.CEILING;
            case "none": return ItemType.NONE;
            default: return ItemType.NONE;
        }
    }


    private GemType StringToGem(string gemType)
    {
        switch (gemType.ToLower())
        {
            case "일반": return GemType.NORMAL;
            case "특수": return GemType.SPECIAL;
            default: return GemType.NORMAL;
        }
    }
}

public struct ShopItemData
    {
        public string itemName;
        public int price;
        public ItemType itemType;
        public GemType gemType;
        public Sprite itemImg;
        public int maxDailyPurchase;
        public int maxTotalPurchase;
        public bool isUnlimited;
    }

    public enum ItemType
    {
        WALL,
        FLOOR,
        WALLFLOOR,
        CEILING,
        SLOT,
        NONE
    }

    public enum GemType
    {
        NORMAL,
        SPECIAL
    }

