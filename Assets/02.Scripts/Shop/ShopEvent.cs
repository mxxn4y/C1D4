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
    private Dictionary<ShopItemData,int> selectedItems = new Dictionary<ShopItemData,int>();
 
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

    private void InitializeItems() // CSV���Ͽ��� 
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

    // IPointerClickHandler ����
    public void OnPointerClick(PointerEventData eventData)
    {
        string indexName = eventData.pointerCurrentRaycast.gameObject.name;
        switch (indexName)
        {
            case "Index1":
                ShopUI.Instance.UpdateItemList(normalItem);
                ShopUI.Instance.ResetTotalPrice();
                Debug.Log("�ε���1");
                break;

            case "Index2":
                ShopUI.Instance.UpdateItemList(specialItem);
                ShopUI.Instance.ResetTotalPrice();
                Debug.Log("�ε���2");
                break;

            case "Index3":
                ShopUI.Instance.UpdateItemList(slotItem);
                ShopUI.Instance.ResetTotalPrice();
                Debug.Log("�ε���3");
                break;
        }
        selectedItems.Clear();
    }
 
    /*
    // ������ Ŭ�� �� �Ѿ� ���
    public void OnItemClick(ShopItemData _itemData)
    {
     
        //if (ShopManager.Instance.CanPurchaseItem(_itemData))
        {
            totalPrice += _itemData.price;
            ShopUI.Instance.UpdatePurchaseButtonText(totalPrice);
            selectedItems.Add(_itemData);
            // �ؿ� ���߿� ���� 
            for (int i = 0; i < selectedItems.Count; i++)
            {
                Debug.Log(selectedItems[i]);
            }
        }
        // ���⿡ ���� Ŭ���� ī�带 add
        //SellingCardUI.Instance.SetCardUI(_itemData);
    }
    */



    public void OnItemClick(ShopItemData _itemData)
    {
 
        totalPrice += _itemData.price;
        //selectedItems[_itemData] = selectedItems.ContainsKey(_itemData) ? selectedItems[_itemData] + 1 : 1;


        foreach (var item in selectedItems)
        {
            Debug.Log("1"+item.Key.itemName);
        }
        SelectedItemsUI.Instance.AddItemToInventory(_itemData);
        //SelectedItemsUI.Instance.UpdateInventoryPanel();
        ShopUI.Instance.UpdatePurchaseButtonText(totalPrice);

        foreach (var item in SelectedItemsUI.Instance.selectedItems)
        {
            Debug.Log($"Item: {item.Key.itemName}, Quantity: {item.Value}");
        }

    }

    public Dictionary<ShopItemData, int> GetDictionary()
    {
        return selectedItems;
    }

    public void OnInventoryItemClick(ShopItemData _itemData)
    {
        if (selectedItems.ContainsKey(_itemData))
        {
            totalPrice -= _itemData.price;
            selectedItems[_itemData]--;

            if (selectedItems[_itemData] <= 0)
            {
                selectedItems.Remove(_itemData);
            }

            //SelectedItemsUI.Instance.UpdateInventoryPanel(selectedItems);
            ShopUI.Instance.UpdatePurchaseButtonText(totalPrice);
        }
        //SelectedItemsUI.Instance.UpdateInventoryPanel();
    }

    // �����ϱ� ��ư Ŭ�� ��
    private void OnPurchase()
    {
        // ���� ó�� ����
        foreach (var item in selectedItems)
        {
            if (item.Key.gemType == GemType.NORMAL && ShopManager.Instance.gem >= totalPrice)
            {
                ShopManager.Instance.UpdateNormalGem(totalPrice);
            }
            else if (item.Key.gemType == GemType.SPECIAL && ShopManager.Instance.specialGem >= totalPrice)
            {
                ShopManager.Instance.UpdateSpecialGem(totalPrice);
            }
            else
            {
                Debug.Log("��ȭ�� �����մϴ�.");
                return;
            }
        }
        Debug.Log("�� ���� �ݾ�: " + totalPrice);
        ShopUI.Instance.ResetTotalPrice();
        selectedItems.Clear();
       // ShopUI.Instance.ClearInventoryPanel();
    }
  
}
