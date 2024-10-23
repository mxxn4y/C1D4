using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Reflection;

public class SellingCardUI : MonoBehaviour/*, IPointerClickHandler*/
{
    public static SellingCardUI Instance { get; private set; }
    public TMP_Text titleTxt;
    public TMP_Text costTxt;
    //public TMP_Text gemTxt;
    public Image tempImg;
    //public Image gemType;
    //public ShopItemSO itemData;

    //private void Awake()
    //{
    //    if (Instance != null && Instance != this)
    //    {
    //        Destroy(this.gameObject);
    //    }
    //    else
    //    {
    //        Instance = this;
    //    }
    //}

    public void SetCardUI(ShopItemData _data)
    {
        //itemData = _item;
        /*titleTxt.text = _item.itemName;
        costTxt.text = _item.price.ToString();
        tempImg.sprite = _item.itemImg;
        */
        SetCardText(_data);
        //SetCardImage(_data);
    }

    public void SetCardText(ShopItemData _data)
    {
        titleTxt.text = _data.itemName;
        costTxt.text = _data.price.ToString();
    }

    public void SetCardImage(ShopItemData _data)
    {

    }
}
