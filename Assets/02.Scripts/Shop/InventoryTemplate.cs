using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryTemplate : MonoBehaviour
{
    public TMP_Text itemNameText;
    public TMP_Text itemQuantityText;
    public Image itemImage;

    public void SetItem(ShopItemSO item, int quantity)
    {
        itemNameText.text = item.itemName;
        itemQuantityText.text = "Quantity: " + quantity.ToString();
        itemImage.sprite = item.itemImg;
    }
}
