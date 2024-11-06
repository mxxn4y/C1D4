using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance { get; private set; }

    public GameObject inventoryPanel;
    public GameObject inventoryItemPrefab;
    private List<ShopItemSO> itemList = new List<ShopItemSO>();

    private Dictionary<ShopItemSO, GameObject> inventoryItems = new Dictionary<ShopItemSO, GameObject>(); //UI프리팹과 매칭
    private Dictionary<ShopItemSO, int> itemCounts = new Dictionary<ShopItemSO, int>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        UpdateInventoryUI();
    }

    private void Update()
    {
        UpdateInventoryUI();
    }

    public void AddItem(ShopItemSO _item) // 구매되면 우선 리스트에 삽입
    {
        itemList.Add(_item);
        Debug.Log("인벤토리에 넣음");

        Debug.Log(itemList.Count);
    }

    public void UpdateInventoryUI()
    {
        //ClearInventoryUI();

        itemCounts.Clear();

        // 리스트에 있는 각각의 아이템의 개수 셈
        foreach (var item in itemList)
        {
            if (itemCounts.ContainsKey(item))
            { itemCounts[item]++; }
            else
            {
                itemCounts[item] = 1;
            }
        }

        foreach (var item in itemCounts)
        {
            GameObject itemGO;
            if (!inventoryItems.ContainsKey(item.Key))
            {
                itemGO = Instantiate(inventoryItemPrefab, inventoryPanel.transform);
                Debug.Log("Create");
                inventoryItems[item.Key] = itemGO;
            }
            else
            {
                itemGO = inventoryItems[item.Key];
            }

            var itemUI = itemGO.GetComponent<PurchasedCardUI>();
            //itemUI.SetItem(item.Key, item.Value);
        }
}

    public Dictionary<ShopItemSO, int> GetInventoryDic()
    {
        return new Dictionary<ShopItemSO, int>(itemCounts);
    }

    private void ClearInventoryUI()
    {
        foreach (Transform child in inventoryPanel.transform)
        {
            Destroy(child.gameObject);
        }
        inventoryItems.Clear();
    }
}
