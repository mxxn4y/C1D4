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
            selectedItems.Add(item,1);
        }

        // InventoryPanel을 업데이트
        /*
        GameObject cardUI = Instantiate(purchasedCard, verticalLayout);
        PurchasedCardUI cardUIComponent = cardUI.GetComponent<PurchasedCardUI>(); 
        cardUIComponent.SetItem(item, _selectedItems[item]);
        */

        UpdateInventoryPanel();
    }


    //    public void UpdateInventoryPanel(ShopItemData item)
    //    {
    //        foreach (Transform items in verticalLayout)
    //        {
    //            Destroy(items.gameObject);  // 기존 아이템 삭제
    //        }
    ///*
    //        foreach (Transform child in InventoryPanel.transform)
    //        {
    //            PurchasedCardUI cardUIComponent = child.GetComponent<PurchasedCardUI>();
    //            if (cardUIComponent != null && cardUIComponent.GetItem().ID == item.ID)
    //            { cardUIComponent.SetItem(item, _selectedItems[item]); }
    //        }
    //*/

    //        foreach (var itemData in item)
    //        {
    //            GameObject cardUI = Instantiate(purchasedCard, verticalLayout);
    //            PurchasedCardUI cardUIComponent = cardUI.GetComponent<PurchasedCardUI>();
    //            cardUIComponent.SetItem(itemData, _selectedItems[itemData]);
    //        }
    //    }


    public void UpdateInventoryPanel()
    {

        //foreach (Transform item in verticalLayout)
        //{
        //    Destroy(item.gameObject);  // 기존 아이템 삭제
        //}

        //List<KeyValuePair<ShopItemData, int>> itemList = new List<KeyValuePair<ShopItemData, int>>(selectedItems);

        foreach (var item in selectedItems)
        {
            Debug.Log("SelectedUI"+item.Key.itemName);
            GameObject cardUI = Instantiate(purchasedCard, verticalLayout);
            //PurchasedCardUI cardUIComponent = cardUI.GetComponent<PurchasedCardUI>();
            //cardUIComponent.SetItem(item.Key, item.Value);
            PurchasedCardUI.Instance.SetItem(item.Key, item.Value);
        }
    }

}
