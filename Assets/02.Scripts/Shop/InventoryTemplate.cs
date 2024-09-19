using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryTemplate : MonoBehaviour
{
    public TMP_Text itemNameText;
    public TMP_Text itemQuantityText;
    public TMP_Text itemGemTypeText;
    public Image itemImage;

    public void SetItem(ShopItemSO item, int quantity)
    {
        itemNameText.text = item.itemName;
        itemQuantityText.text = "Quantity: " + quantity.ToString();
        itemImage.sprite = item.itemImg;

        if (item.gemType == ShopItemSO.GemType.NORMAL)
        {
            itemGemTypeText.text = "일반";
        }
        else if (item.gemType == ShopItemSO.GemType.SPECIAL)
        {
            itemGemTypeText.text = "특수";
        }
    }
}
