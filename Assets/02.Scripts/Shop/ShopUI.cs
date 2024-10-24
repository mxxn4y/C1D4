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
            Destroy(item.gameObject);  // ���� ������ ����
        }

        foreach (ShopItemData itemData in items)
        {
            GameObject newItem = Instantiate(cardPrefab, itemGrid);
            newItem.name = itemData.itemName;
            SellingCardUI cardUI = newItem.GetComponent<SellingCardUI>();
            cardUI.SetCardUI(itemData);
            newItem.AddComponent<Button>().onClick.AddListener(() => ShopEvent.Instance.OnItemClick(itemData));  // Ŭ�� �� ������ �߰�
        }
    }

    public void UpdatePurchaseButtonText(int _amount)
    {
        purchaseButtonText.text = "�����ϱ� " + _amount.ToString();
    }

    public void ResetTotalPrice()
    {
        ShopEvent.Instance.totalPrice = 0;
        purchaseButtonText.text = "�����ϱ� ";
    }

    public void CloseShop()
    {
        shopUI.SetActive(false);  // ���� UI�� ��Ȱ��ȭ
    }
}
