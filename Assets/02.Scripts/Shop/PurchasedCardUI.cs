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
        itemQuantityText.text = _quantity.ToString() + " ��";
        SetItemImg(_item);
        //itemImg.sprite = _item.itemImg;
        Debug.Log("setItem�Լ� ");
        if (_item.gemType == GemType.NORMAL)
        {
            itemGemTypeText.text = "�Ϲ�";
            //GemTypeImg.color = new Color(150 / 255f, 107 / 255f, 81 / 255f);
        }
        else if (_item.gemType == GemType.SPECIAL)
        {
            itemGemTypeText.text = "Ư��";
            //GemTypeImg.color = new Color(149 / 255f, 97 / 255f, 166 / 255f);
        }
        GetComponent<Button>().onClick.AddListener(() => OnRemoveItemClick(_item));
    }

    private void OnRemoveItemClick(ShopItemData _item)
    {
        ShopEvent.Instance.RemoveItemFromInventory(_item);  // ���� ���
        Destroy(gameObject);  // ���� ������ �ν��Ͻ� ����
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
                Debug.Log("��ġ�ϴ� ������ ������");
            }
        }
    }

    public ShopItemData GetItem()
    {
        return currentItem;
    }
}
