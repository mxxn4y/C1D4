using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ShopMenu", menuName = "Scriptable Objects/New Shop Item", order = 1)]
public class ShopItemSO : ScriptableObject
{

    public string itemName;
    public int price;
    public ItemType itemType;
    public GemType gemType;
    public Sprite itemImg;
    public int maxDailyPurchase;
    public int maxTotalPurchase;
    public bool isUnlimited;
    //public string gemTypeTxt;

    public string ItemName { get { return itemName; } }
    public int Price { get  { return price; } }
    public Sprite ItemImg { get  { return itemImg; } }
    //public string GemTypeTxt { get { return gemTypeTxt; } }

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

    public ItemType StringToItem(string _itemArray)
    {
        return (ItemType)Enum.Parse(typeof(ItemType), _itemArray);
    }

    public GemType StringToGem(string _gemArray)
    {
        //return (GemType)Enum.Parse(typeof(GemType), _gemArray);
        switch (_gemArray)
        {
            case "�Ϲ�":
                return GemType.NORMAL;
            case "Ư��":
                return GemType.SPECIAL;
            default:
                throw new ArgumentException($"Invalid gem type: {_gemArray}");
        }
    }

    //public string GemTypeText(string _gemArray)
    //{
    //    switch (_gemArray)
    //    {
    //        case "�Ϲ�":
    //            return "�Ϲ�";
    //        case "Ư��":
    //            return "Ư��";
    //        default:
    //            throw new ArgumentException("�Ϲ�, Ư�� ��ȭ �ƴ�");
    //    }
    //}
}
