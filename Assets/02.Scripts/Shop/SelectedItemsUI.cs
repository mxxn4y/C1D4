using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedItemsUI : MonoBehaviour
{

    public static SelectedItemsUI Instance;

    public Transform verticalLayout;
    public GameObject InventoryPanel;
    public GameObject purchasedCard;
    public Dictionary<ShopItemData, int> selectedItems = new Dictionary<ShopItemData, int>();
    private Dictionary<GameObject, int> selectedItemsPrefab = new Dictionary<GameObject, int>();


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


    public void AddItemToInventory(ShopItemData item)
    {

        // 선택된 아이템이 이미 있으면 수량을 증가
        if (selectedItems.ContainsKey(item))
        {
            selectedItems[item]++;
        }
        else
        {
            // 새로운 아이템이면 딕셔너리에 추가
            selectedItems.Add(item, 1);
        }

        UpdateInventoryPanel();
    }

    public void UpdateInventoryPanel()
    {

        foreach (Transform item in verticalLayout)
        {
            Destroy(item.gameObject);  // 기존 아이템 삭제
        }

        foreach (var item in selectedItems)
        {

            Debug.Log("SelectedUI" + item.Key.itemName + " 수량: " + item.Value);

            GameObject cardUI = Instantiate(purchasedCard, verticalLayout);
            cardUI.name = item.Key.itemName;
            PurchasedCardUI cardUIComponent = cardUI.GetComponent<PurchasedCardUI>();
            cardUIComponent.SetItem(item.Key, item.Value);
            Debug.Log("Instantiated cardUI with item: " + item.Key.itemName + " 수량: " + item.Value);

        }

    }

    public void ClearInventoryPanel()
    {

        foreach (Transform item in verticalLayout)
        {
            Destroy(item.gameObject);  // 기존 아이템 삭제
        }
    }

}
