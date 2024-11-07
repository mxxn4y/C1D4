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

        // ���õ� �������� �̹� ������ ������ ����
        if (selectedItems.ContainsKey(item))
        {
            selectedItems[item]++;
        }
        else
        {
            // ���ο� �������̸� ��ųʸ��� �߰�
            selectedItems.Add(item, 1);
        }

        UpdateInventoryPanel();
    }

    public void UpdateInventoryPanel()
    {

        foreach (Transform item in verticalLayout)
        {
            Destroy(item.gameObject);  // ���� ������ ����
        }

        foreach (var item in selectedItems)
        {

            Debug.Log("SelectedUI" + item.Key.itemName + " ����: " + item.Value);

            GameObject cardUI = Instantiate(purchasedCard, verticalLayout);
            cardUI.name = item.Key.itemName;
            PurchasedCardUI cardUIComponent = cardUI.GetComponent<PurchasedCardUI>();
            cardUIComponent.SetItem(item.Key, item.Value);
            Debug.Log("Instantiated cardUI with item: " + item.Key.itemName + " ����: " + item.Value);

        }

    }

    public void ClearInventoryPanel()
    {

        foreach (Transform item in verticalLayout)
        {
            Destroy(item.gameObject);  // ���� ������ ����
        }
    }

}
