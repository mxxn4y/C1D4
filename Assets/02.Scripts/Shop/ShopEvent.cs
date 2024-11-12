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
        SelectedItemsUI.Instance.selectedItems.Clear();
        SelectedItemsUI.Instance.ClearInventoryPanel();
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
        if (!ShopManager.Instance.CanPurchaseItem(_itemData))
        {
            Debug.Log($"{_itemData.itemName}��(��) ���� ���ѿ� �����߽��ϴ�.");
            return;
        }
        totalPrice += _itemData.price;

        SelectedItemsUI.Instance.AddItemToInventory(_itemData);
        ShopUI.Instance.UpdatePurchaseButtonText(totalPrice);

        foreach (var item in SelectedItemsUI.Instance.selectedItems)
        {
            Debug.Log($"Item: {item.Key.itemName}, Quantity: {item.Value}");
        }

    }

    public void OnInventoryItemClick(ShopItemData _itemData)
    {
        if (SelectedItemsUI.Instance.selectedItems.ContainsKey(_itemData))
        {
            totalPrice -= _itemData.price;
            SelectedItemsUI.Instance.selectedItems[_itemData]--;

            if (SelectedItemsUI.Instance.selectedItems[_itemData] <= 0)
            {
                SelectedItemsUI.Instance.selectedItems.Remove(_itemData);
                //Destroy()
            }

            //SelectedItemsUI.Instance.UpdateInventoryPanel(selectedItems);
            //ShopUI.Instance.UpdatePurchaseButtonText(totalPrice);
        }
        SelectedItemsUI.Instance.UpdateInventoryPanel();
        ShopUI.Instance.UpdatePurchaseButtonText(totalPrice);
    }

    public void RemoveItemFromInventory(ShopItemData _itemData)
    {
        if (SelectedItemsUI.Instance.selectedItems.ContainsKey(_itemData))
        {
            totalPrice -= _itemData.price;  // �Ѿ׿��� ������ ��
            SelectedItemsUI.Instance.selectedItems[_itemData]--;

            // ������ 0�� �Ǹ� ��ųʸ����� �������� ����
            if (SelectedItemsUI.Instance.selectedItems[_itemData] <= 0)
            {
                SelectedItemsUI.Instance.selectedItems.Remove(_itemData);
            }

            // UI ����
            SelectedItemsUI.Instance.UpdateInventoryPanel();
            ShopUI.Instance.UpdatePurchaseButtonText(totalPrice);
        }
    }

    // �����ϱ� ��ư Ŭ�� ��
    private void OnPurchase()
    {
        // ��ü ���� ���� ���� ���� Ȯ��
        if (totalPrice > ShopManager.Instance.gem && totalPrice > ShopManager.Instance.specialGem)
        {
            Debug.Log("��ȭ�� �����մϴ�.");
            return;
        }

        // ���� ���� ó�� ����
        foreach (var item in SelectedItemsUI.Instance.selectedItems)
        {
            int itemTotalPrice = item.Key.price * item.Value; // ������ ���� * ����
            if (item.Key.gemType == GemType.NORMAL)
            {
                if (ShopManager.Instance.gem >= itemTotalPrice)
                {
                    ShopManager.Instance.UpdateNormalGem(itemTotalPrice);
                }
                else
                {
                    Debug.Log("��ȭ�� �����մϴ�.");
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
                    Debug.Log("��ȭ�� �����մϴ�.");
                    return;
                }
            }
            for (int i = 0; i < item.Value; i++) // �������� ������ŭ �ݺ�
            {
                ShopManager.Instance.TrackPurchase(item.Key);
            }
        }

        Debug.Log("�� ���� �ݾ�: " + totalPrice);
        ShopUI.Instance.ResetTotalPrice();
        SelectedItemsUI.Instance.selectedItems.Clear();
        SelectedItemsUI.Instance.ClearInventoryPanel();
    }


}
