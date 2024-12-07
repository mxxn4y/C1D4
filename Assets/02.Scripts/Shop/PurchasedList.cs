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

    private List<ShopItemData> purchasedList = new List<ShopItemData>(); //�ʿ���� ��
    public Dictionary<ShopItemData, int> purchasedDic = new Dictionary<ShopItemData, int>();

    public void AddItemToPurchasedDic(ShopItemData item)
    {

        // ���õ� �������� �̹� ������ ������ ����
        if (purchasedDic.ContainsKey(item))
        {
            purchasedDic[item]++;
        }
        else
        {
            // ���ο� �������̸� ��ųʸ��� �߰�
            purchasedDic.Add(item, 1);
        }
    }

    private void ItemEffect()
    {
        
    }
}
