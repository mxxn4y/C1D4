using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ShopUI : MonoBehaviour
{
    public static ShopUI Instance { get; private set; }

    public Transform itemGrid;
    public Button purchaseButton;
    public TextMeshProUGUI purchaseButtonText;
    public GameObject shopUI;
    public Button closeButton;
    public GameObject cardPrefab;


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

    private void Start()
    {
        // Initialize the shop UI elements here if needed
    }

    public void UpdateItemList(List<ShopItemData> items)
    {
        foreach (Transform item in itemGrid)
        {
            Destroy(item.gameObject);  // 기존 아이템 삭제
        }

        foreach (ShopItemData itemData in items)
        {
            GameObject newItem = Instantiate(cardPrefab, itemGrid);
            newItem.name = itemData.itemName;
            SellingCardUI cardUI = newItem.GetComponent<SellingCardUI>();
            cardUI.SetCardUI(itemData);
            newItem.AddComponent<Button>().onClick.AddListener(() => ShopEvent.Instance.OnItemClick(itemData));  // 클릭 시 아이템 추가
        }
    }

    public void UpdatePurchaseButtonText(int _amount)
    {
        purchaseButtonText.text = "구매하기 " + _amount.ToString();
    }

    public void ResetTotalPrice()
    {
        ShopEvent.Instance.totalPrice = 0;
        purchaseButtonText.text = "구매하기 ";
    }

    public void CloseShop()
    {
        shopUI.SetActive(false);  // 상점 UI를 비활성화
    }
}
