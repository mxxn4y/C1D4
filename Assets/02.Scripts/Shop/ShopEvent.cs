using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ShopEvent : MonoBehaviour, IPointerClickHandler
{
    public static ShopEvent Instance { get; private set; }

    public List<ShopItemData> normalItem=new List<ShopItemData>(); 
    public List<ShopItemData> specialItem=new List<ShopItemData>();
    public List<ShopItemData> slotItem=new List<ShopItemData>();

    public int totalPrice = 0;
    private List<ShopItemData> selectedItems = new List<ShopItemData>();
 
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
                Debug.Log("인덱스1");
                break;

            case "Index2":
                ShopUI.Instance.UpdateItemList(specialItem);
                ShopUI.Instance.ResetTotalPrice();
                Debug.Log("인덱스2");
                break;

            case "Index3":
                ShopUI.Instance.UpdateItemList(slotItem);
                ShopUI.Instance.ResetTotalPrice();
                Debug.Log("인덱스3");
                break;
        }
        selectedItems.Clear();
    }
 
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

    // 구매하기 버튼 클릭 시
    private void OnPurchase()
    {
        // 구매 처리 로직
        foreach (var item in selectedItems)
        {
            if (item.gemType == GemType.NORMAL && ShopManager.Instance.gem >= totalPrice)
            {
                ShopManager.Instance.UpdateNormalGem(totalPrice);
            }
            else if (item.gemType == GemType.SPECIAL && ShopManager.Instance.specialGem >= totalPrice)
            {
                ShopManager.Instance.UpdateSpecialGem(totalPrice);
            }
            else
            {
                Debug.Log("재화가 부족합니다.");
                return;
            }
        }
        Debug.Log("총 구매 금액: " + totalPrice);
        ShopUI.Instance.ResetTotalPrice();
        selectedItems.Clear();
    }
  
}
