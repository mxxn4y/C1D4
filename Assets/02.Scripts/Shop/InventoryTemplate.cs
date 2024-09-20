using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Reflection;

public class InventoryTemplate : MonoBehaviour
{
    public TMP_Text itemNameText;
    public TMP_Text itemQuantityText;
    public TMP_Text itemGemTypeText;
    public Image GemType;
    public Image itemImage;
    

    public void SetItem(ShopItemSO item, int quantity)
    {
        itemNameText.text = item.itemName;
        itemQuantityText.text = quantity.ToString()+" 개";
        itemImage.sprite = item.itemImg;

        if (item.gemType == ShopItemSO.GemType.NORMAL)
        {
            itemGemTypeText.text = "일반";
            GemType.color = new Color(150 / 255f, 107 / 255f, 81 / 255f);
        }
        else if (item.gemType == ShopItemSO.GemType.SPECIAL)
        {
            itemGemTypeText.text = "특수";
            GemType.color = new Color(149 / 255f, 97 / 255f, 166 / 255f);
        }
    }
}
