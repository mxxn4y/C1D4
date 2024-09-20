using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{

    public GameObject inventoryPanel;
    public GameObject inventoryItemPrefab;

    // ¾ÆÀÌÅÛ°ú ½½·Ô ¸ÅÄª µñ¼Å³Ê¸®
    private Dictionary<ShopItemSO, GameObject> inventoryItems = new Dictionary<ShopItemSO, GameObject>();

    private void Start()
    {
        UpdateInventoryUI();
    }

    private void Update()
    {
        UpdateInventoryUI();
    }

    public void UpdateInventoryUI()
    {
        //ClearInventoryUI();
        var inventory = InventoryManager.Instance.GetInventory();
        Dictionary<ShopItemSO, int> itemCounts = new Dictionary<ShopItemSO, int>();

        // ¾ÆÀÌÅÛ °³¼ö ¼À
        foreach (var item in inventory)
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

            var itemUI = itemGO.GetComponent<InventoryTemplate>();
            itemUI.SetItem(item.Key, item.Value);
        }

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
