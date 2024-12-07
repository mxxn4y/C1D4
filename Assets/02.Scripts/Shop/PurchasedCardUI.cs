using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Reflection;

public class PurchasedCardUI : MonoBehaviour // PurchasedCardUI
{

    public TMP_Text itemNameText;
    public TMP_Text itemQuantityText;
    public TMP_Text itemGemTypeText;
    //public Image GemTypeImg;
    public Image itemImg;
    private ShopItemData currentItem;

    public void SetItem(ShopItemData _item, int _quantity)
    {
        itemNameText.text = _item.itemName;
        itemQuantityText.text = _quantity.ToString() + " 개";
        SetItemImg(_item);
        //itemImg.sprite = _item.itemImg;
        Debug.Log("setItem함수 ");
        if (_item.gemType == GemType.NORMAL)
        {
            itemGemTypeText.text = "일반";
            //GemTypeImg.color = new Color(150 / 255f, 107 / 255f, 81 / 255f);
        }
        else if (_item.gemType == GemType.SPECIAL)
        {
            itemGemTypeText.text = "특수";
            //GemTypeImg.color = new Color(149 / 255f, 97 / 255f, 166 / 255f);
        }
        GetComponent<Button>().onClick.AddListener(() => OnRemoveItemClick(_item));
    }

    private void OnRemoveItemClick(ShopItemData _item)
    {
        ShopEvent.Instance.RemoveItemFromInventory(_item);  // 선택 취소
        Destroy(gameObject);  // 현재 프리팹 인스턴스 제거
    }

    private void SetItemImg(ShopItemData _item)
    {
        Sprite[] shopitemimgs = Resources.LoadAll<Sprite>("ShopItems/ShopItemImgs");
        foreach (var img in shopitemimgs)
        {
            if (img.name.Equals(_item.itemName))
            {
                itemImg.sprite = img;
                Debug.Log("SetDefaultImg : " + img.name);
            }
            else
            {
                Debug.Log("일치하는 사진이 없습니");
            }
        }
    }

    public ShopItemData GetItem()
    {
        return currentItem;
    }
}
