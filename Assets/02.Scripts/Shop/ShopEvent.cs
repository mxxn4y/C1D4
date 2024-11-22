using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


public class ShopEvent : MonoBehaviour, IPointerClickHandler
{
    public static ShopEvent Instance { get; private set; }

    public List<ShopItemData> normalItem = new List<ShopItemData>();
    public List<ShopItemData> specialItem = new List<ShopItemData>();
    public List<ShopItemData> slotItem = new List<ShopItemData>();

    public int totalPrice = 0;

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
        ShopUI.Instance.purchaseButton.onClick.AddListener(OnPurchase);
        ShopUI.Instance.closeButton.onClick.AddListener(ShopUI.Instance.CloseShop);
    }

    private void Start()
    {
        InitializeItems();

        if (normalItem.Count > 0)
        {
            ShopUI.Instance.UpdateItemList(normalItem);
        }
        else
        {
            ShopUI.Instance.UpdateItemList(normalItem);
            //ShopUI.Instance.purchaseButton.onClick.AddListener(OnPurchase);
            //ShopUI.Instance.closeButton.onClick.AddListener(ShopUI.Instance.CloseShop);

        }
    }

    private void InitializeItems() // CSV파일에서 
    {

        if (ShopTable.Instance.shopCSV == null || ShopTable.Instance.shopCSV.Count == 0)
        {
            Debug.LogError("shopCSV 데이터가 없습니다.");
            return;
        }

        foreach (var data in ShopTable.Instance.shopCSV)
        {
            ShopItemData itemData = ShopTable.Instance.GetData(data["itemName"].ToString());
            if (itemData.itemType != ItemType.SLOT)
            {
                switch (itemData.gemType)
                {
                    case GemType.NORMAL:
                        normalItem.Add(itemData);
                        break;
                    case GemType.SPECIAL:
                        specialItem.Add(itemData);
                        break;
                }
            }
            else
            {
                slotItem.Add(itemData);
            }
        }
        Debug.Log($"Normal Item Count: {normalItem.Count}");
        Debug.Log($"Special Item Count: {specialItem.Count}");
        Debug.Log($"Slot Item Count: {slotItem.Count}");
    }

    // IPointerClickHandler 구현
    public void OnPointerClick(PointerEventData eventData)
    {
        string indexName = eventData.pointerCurrentRaycast.gameObject.name;
        switch (indexName)
        {
            case "Index1":
                ShopUI.Instance.UpdateItemList(normalItem);
                ShopUI.Instance.ResetTotalPrice();
                SelectedItemsUI.Instance.selectedItems.Clear();
                SelectedItemsUI.Instance.ClearInventoryPanel();
                Debug.Log("인덱스1");
                break;

            case "Index2":
                ShopUI.Instance.UpdateItemList(specialItem);
                ShopUI.Instance.ResetTotalPrice();
                SelectedItemsUI.Instance.selectedItems.Clear();
                SelectedItemsUI.Instance.ClearInventoryPanel();
                Debug.Log("인덱스2");
                break;

            case "Index3":
                ShopUI.Instance.UpdateItemList(slotItem);
                ShopUI.Instance.ResetTotalPrice();
                SelectedItemsUI.Instance.selectedItems.Clear();
                SelectedItemsUI.Instance.ClearInventoryPanel();
                Debug.Log("인덱스3");
                break;
        }
    }

    /*
    // 아이템 클릭 시 총액 계산
    public void OnItemClick(ShopItemData _itemData)
    {
     
        //if (ShopManager.Instance.CanPurchaseItem(_itemData))
        {
            totalPrice += _itemData.price;
            ShopUI.Instance.UpdatePurchaseButtonText(totalPrice);
            selectedItems.Add(_itemData);
            // 밑에 나중에 삭제 
            for (int i = 0; i < selectedItems.Count; i++)
            {
                Debug.Log(selectedItems[i]);
            }
        }
        // 여기에 구매 클릭한 카드를 add
        //SellingCardUI.Instance.SetCardUI(_itemData);
    }
    */


    
    public void OnItemClick(ShopItemData _itemData)
    {
        if (!ShopManager.Instance.CanClickItem(_itemData))
        {
            Debug.Log($"{_itemData.itemName}은(는) 구매 제한에 도달했습니다.");
            return;
        }
        if (ShopManager.Instance.CanClickItem(_itemData))
        {
            totalPrice += _itemData.price;

            SelectedItemsUI.Instance.AddItemToInventory(_itemData);
            ShopUI.Instance.UpdatePurchaseButtonText(totalPrice);
            ShopManager.Instance.TrackPurchase(_itemData);
        }
        
        foreach (var item in SelectedItemsUI.Instance.selectedItems)
        {
            Debug.Log($" 추가할 때 selected딕셔너리 Item: {item.Key.itemName}, Quantity: {item.Value}");
            //ShopManager.Instance.TrackPurchase(item.Key);
        }

    }
    /*
    public void OnItemClick(ShopItemData _itemData)
    {
        // 현재 선택된 아이템 수량 확인
        int currentlySelected = SelectedItemsUI.Instance.selectedItems.ContainsKey(_itemData)
            ? SelectedItemsUI.Instance.selectedItems[_itemData]
            : 0;

        // CanPurchaseItem에 현재 선택된 수량을 전달
        if (!ShopManager.Instance.CanPurchaseItem(_itemData, currentlySelected + 1))
        {
            Debug.Log($"{_itemData.itemName}은(는) 구매 제한에 도달했습니다.");
            return;
        }

        // 구매 가능하면 선택한 아이템에 추가
        totalPrice += _itemData.price;
        SelectedItemsUI.Instance.AddItemToInventory(_itemData);
        ShopUI.Instance.UpdatePurchaseButtonText(totalPrice);

        Debug.Log($"selected딕셔너리 안의 Item: {_itemData.itemName}, Quantity: {currentlySelected + 1}");
    }

    */

    public void RemoveItemFromInventory(ShopItemData _itemData)
    {
       
        if (SelectedItemsUI.Instance.selectedItems.ContainsKey(_itemData))
        {
            totalPrice -= _itemData.price;  // 총액에서 가격을 뺌
            SelectedItemsUI.Instance.selectedItems[_itemData]--;

            // 수량이 0이 되면 딕셔너리에서 아이템을 제거
            if (SelectedItemsUI.Instance.selectedItems[_itemData] <= 0)
            {
                SelectedItemsUI.Instance.selectedItems.Remove(_itemData);
            }
           
            ShopManager.Instance.RemovePurchase(_itemData);
            // UI 갱신
            SelectedItemsUI.Instance.UpdateInventoryPanel();
            ShopUI.Instance.UpdatePurchaseButtonText(totalPrice);
       
        }
        foreach (var item in SelectedItemsUI.Instance.selectedItems)
        {
            Debug.Log($" 제거되고 나서 selected딕셔너리 Item: {item.Key.itemName}, Quantity: {item.Value}");
        }
    }

    // 구매하기 버튼 클릭 시
    private void OnPurchase()
    {
        // 전체 구매 가능 여부 먼저 확인
        if (totalPrice > ShopManager.Instance.gem && totalPrice > ShopManager.Instance.specialGem)
        {
            Debug.Log("재화가 부족합니다.");
            return;
        }

        // 실제 구매 처리 로직
        
        foreach (var item in SelectedItemsUI.Instance.selectedItems)
        {
            int itemTotalPrice = item.Key.price * item.Value; // 아이템 가격 * 수량
            if (item.Key.gemType == GemType.NORMAL)
            {
                if (ShopManager.Instance.gem >= itemTotalPrice)
                {
                    ShopManager.Instance.UpdateNormalGem(itemTotalPrice);
                }
                else
                {
                    Debug.Log("재화가 부족합니다.");
                    return;
                }
            }
            else if (item.Key.gemType == GemType.SPECIAL)
            {
                if (ShopManager.Instance.specialGem >= itemTotalPrice)
                {
                    ShopManager.Instance.UpdateSpecialGem(itemTotalPrice);
                }
                else
                {
                    Debug.Log("재화가 부족합니다.");
                    return;
                }
            }
            //for (int i = 0; i < item.Value; i++) // 아이템의 수량만큼 반복
            //{
            //    ShopManager.Instance.TrackPurchase(item.Key);
            //}
        }

        Debug.Log("총 구매 금액: " + totalPrice);
        ShopUI.Instance.ResetTotalPrice();
        SelectedItemsUI.Instance.selectedItems.Clear();
        SelectedItemsUI.Instance.ClearInventoryPanel();
    }


}
