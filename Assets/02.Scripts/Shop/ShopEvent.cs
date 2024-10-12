using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
//ShopTable���� itemData �޾Ƽ� GemType.NORMAL�� �������� normalItem�� �ְ�, GemTyoe.SPECIAL�� �������� specialItem�� �ְ�,ItemType�� SLOT�� �������� slotItem�� �־�� ��. �׷� ������ Index1�� Ŭ���ϸ� normalItem�ȿ� �ִ� ī�� ������ UI�� ������, Index2�� Ŭ���ϸ� specialItem ī�� ������ UI�� ������, Index3�� Ŭ���ϸ� slotItem ī�� ������ UI������ OnPointerClick�Լ��� UpdateItemList�Լ� ��������

public class ShopEvent : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    public void OnPointerEnter(PointerEventData _event)
    {

    }

    public void OnPointerExit(PointerEventData _event)
    {

    }

    /// public static ShopEvent Instance;

    //public List<ShopItemData> normalItems;  // �Ϲ� ������ ����Ʈ
    //public List<ShopItemData> specialItems; // Ư�� ������ ����Ʈ

    public List<ShopItemData> normalItem=new List<ShopItemData>(); // ����
    public List<ShopItemData> specialItem=new List<ShopItemData>();
    public List<ShopItemData> slotItem=new List<ShopItemData>(); //

    public Transform itemGrid;
    public Button purchaseButton;  // �����ϱ� ��ư
    public TextMeshProUGUI purchaseButtonText;  // �����ϱ� ��ư�� �ؽ�Ʈ
    public GameObject shopUI;  // ���� UI â
    public Button closeButton;
    public GameObject cardPrefab;

    private int totalPrice = 0;
    private List<ShopItemData> currentItems;  // ���� ǥ�� ���� ������ ����Ʈ
    // private List <ShopItemData> currentItems;

    private void Start()
    {
        InitializeItems();

        if (normalItem == null || specialItem == null || slotItem == null)
        {
            Debug.LogError("������ ����Ʈ�� null �����Դϴ�.");
            return;
        }

        if (normalItem.Count > 0)
        {
            UpdateItemList(normalItem);
        }
        else
        {
            Debug.LogError("Normal Item ����Ʈ�� ��� �ֽ��ϴ�.");

            UpdateItemList(normalItem);
            purchaseButton.onClick.AddListener(OnPurchase);
            closeButton.onClick.AddListener(CloseShop);
        }
    }

    private void InitializeItems()
    {

        if (ShopTable.Instance.shopCSV == null || ShopTable.Instance.shopCSV.Count == 0)
        {
            Debug.LogError("shopCSV �����Ͱ� �����ϴ�.");
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

    private void UpdateItemList(List<ShopItemData> items)
    {
        if (items == null)
        {
            Debug.LogError("items ����Ʈ�� null �����Դϴ�.");
            return;
        }

        // itemGrid�� null���� Ȯ��
        if (itemGrid == null)
        {
            Debug.LogError("itemGrid�� null �����Դϴ�.");
            return;
        }

        // cardPrefab�� null���� Ȯ��
        if (cardPrefab == null)
        {
            Debug.LogError("cardPrefab�� null �����Դϴ�.");
            return;
        }
        currentItems = items;
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
            newItem.AddComponent<Button>().onClick.AddListener(() => OnItemClick(itemData));  // Ŭ�� �� ������ �߰�
        }
    }


    // IPointerClickHandler ����
    public void OnPointerClick(PointerEventData eventData)
    {
        string indexName = eventData.pointerCurrentRaycast.gameObject.name;
        switch (indexName)
        {
            case "Index1":
                UpdateItemList(normalItem);
                ResetTotalPrice();
                Debug.Log("�ε���1");
                break;

            case "Index2":
                UpdateItemList(specialItem);
                ResetTotalPrice();
                Debug.Log("�ε���2");
                break;

            case "Index3":
                UpdateItemList(slotItem);
                ResetTotalPrice();
                Debug.Log("�ε���3");
                break;
        }
    }
    /*
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
                //// cardUI.SetCardUI(itemData);
                /// SellingCardUI.SetCardText();

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
    */

    // ������ Ŭ�� �� �Ѿ� ���
    public void OnItemClick(ShopItemData _itemData)
    {
        totalPrice += _itemData.price;
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
        purchaseButtonText.text = "�����ϱ� " + totalPrice;
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
