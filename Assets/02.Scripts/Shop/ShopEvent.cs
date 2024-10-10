using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


public class ShopEvent : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    public void OnPointerEnter(PointerEventData _event)
    {

    }

    public void OnPointerExit(PointerEventData _event)
    {

    }
 
    /// public static ShopEvent Instance;

    public List<ShopItemSO> normalItems;  // �Ϲ� ������ ����Ʈ
    public List<ShopItemSO> specialItems; // Ư�� ������ ����Ʈ
    public Transform itemGrid;  // 4�� �̹����� GridLayout
    public Button purchaseButton;  // �����ϱ� ��ư
    public TextMeshProUGUI purchaseButtonText;  // �����ϱ� ��ư�� �ؽ�Ʈ
    public GameObject shopUI;  // ���� UI â
    public Button closeButton;  // 6�� �̹��� (���� �ݱ� ��ư)
    public GameObject cardPrefab;

    private int totalPrice = 0;
    private List<ShopItemSO> currentItems;  // ���� ǥ�� ���� ������ ����Ʈ

    private void Start()
    {
        UpdateItemList(normalItems);
        purchaseButton.onClick.AddListener(OnPurchase);
        closeButton.onClick.AddListener(CloseShop);

    }

    // IPointerClickHandler ����
    public void OnPointerClick(PointerEventData eventData)
    {
        // �Ϲ� ������ ����
        if (eventData.pointerCurrentRaycast.gameObject.name == "Index1")
        {
            UpdateItemList(normalItems);
            ResetTotalPrice();
            Debug.Log("�ε���1");
        }
        // Ư�� ������ ����
        else if (eventData.pointerCurrentRaycast.gameObject.name == "Index2")
        {
            UpdateItemList(specialItems);
            ResetTotalPrice();
            Debug.Log("�ε���2");
        }
    }

    private void UpdateItemList(List<ShopItemSO> items)
    {
        currentItems = items;
        foreach (Transform item in itemGrid)
        {
            Destroy(item.gameObject);  // ���� ������ ����
        }

        foreach (ShopItemSO itemData in items)
        {
            GameObject newItem = Instantiate(cardPrefab, itemGrid);
            //GameObject newItem = new GameObject();  // ���ο� ������ ������Ʈ ����
            newItem.name = itemData.itemName;
            SellingCardUI cardUI = newItem.GetComponent<SellingCardUI>();

            // ������ ������ ī�忡 ����
            cardUI.SetCardUI(itemData);

            //SellingCardUI cardUI = newItem.GetComponent<SellingCardUI>();
            //if (cardUI != null)
            //{
            //    cardUI.SetCardUI(itemData);  // ������ ������ �ݿ�
            //}

            newItem.AddComponent<Button>().onClick.AddListener(() => OnItemClick(itemData));  // Ŭ�� �� ������ �߰�
            // ���⼭ ������ UI ��Ҹ� �߰��ϼ��� (�ؽ�Ʈ, �̹��� ��)
            //newItem.transform.SetParent(itemGrid);
        }
    }

    // ������ Ŭ�� �� �Ѿ� ���
    public void OnItemClick(ShopItemSO itemData)
    {
        totalPrice += itemData.Price;
        UpdatePurchaseButtonText();

        //SellingCardUI.Instance.SetCardUI(itemData);
    }

    // �����ϱ� ��ư Ŭ�� ��
    private void OnPurchase()
    {
        // ���� ó�� ����
        Debug.Log("�� ���� �ݾ�: " + totalPrice);
        ResetTotalPrice();
    }

    // ���� ��ư �ؽ�Ʈ ������Ʈ
    private void UpdatePurchaseButtonText()
    {
        purchaseButtonText.text = "�����ϱ� " + totalPrice ;
    }

    // �Ѿ� �ʱ�ȭ
    private void ResetTotalPrice()
    {
        totalPrice = 0;
        UpdatePurchaseButtonText();
    }

    // ���� �ݱ�
    private void CloseShop()
    {
        shopUI.SetActive(false);  // ���� UI�� ��Ȱ��ȭ
    }
}
