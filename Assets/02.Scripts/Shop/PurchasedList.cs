using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchasedList : MonoBehaviour
{
    public static PurchasedList Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private List<ShopItemData> purchasedList = new List<ShopItemData>(); //필요없을 듯
    public Dictionary<ShopItemData, int> purchasedDic = new Dictionary<ShopItemData, int>();

    public void AddItemToPurchasedDic(ShopItemData item)
    {

        // 선택된 아이템이 이미 있으면 수량을 증가
        if (purchasedDic.ContainsKey(item))
        {
            purchasedDic[item]++;
        }
        else
        {
            // 새로운 아이템이면 딕셔너리에 추가
            purchasedDic.Add(item, 1);
        }
    }

    private void ItemEffect()
    {
        
    }
}
