using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SellingCardUI : MonoBehaviour
{
    public TMP_Text titleTxt;
    public TMP_Text costTxt;
    public Image cardItemImg;
    public Sprite soldImg;
    private ShopItemData itemData;

    public void SetCardUI(ShopItemData _data)
    {
        itemData = _data;
        UpdateText(_data);
        UpdateImage();
        ShopManager.Instance.OnPurchaseStateChanged += OnPurchaseStateChanged; // ���� ���� ���� �̺�Ʈ ���
    }

    private void OnDestroy()
    {
        // �̺�Ʈ ����
       
        if (ShopManager.Instance != null)
            ShopManager.Instance.OnPurchaseStateChanged -= OnPurchaseStateChanged;
    }

    private void UpdateText(ShopItemData _data)
    {
        titleTxt.text = _data.itemName;
        costTxt.text = _data.price.ToString();
    }

    private void UpdateImage()
    {
        if (ShopManager.Instance.CanClickItem(itemData))
        {
            /*
            Sprite[] shopItemImgs = Resources.LoadAll<Sprite>("");
            foreach(var img in shopItemImgs)
            {
             
                cardItemImg.sprite = img;
            }
            */
            cardItemImg.sprite = itemData.itemImg; // �⺻ �̹����� ���� //������ �ӽ÷� �� �ڵ���
            Debug.Log("�ֵ�ƿ�->�⺻");
        }
        else
        {
            cardItemImg.sprite = soldImg; // �ֵ�ƿ� �̹����� ����
            Debug.Log("�⺻->�ֵ�ƿ�");
        }
    }

    private void OnPurchaseStateChanged(string itemName)
    {
        if (itemData.itemName == itemName)
        {
            UpdateImage();
        }
    }
}

/*
public class SellingCardUI : MonoBehaviour
{
    public TMP_Text titleTxt;
    public TMP_Text costTxt;
    public Image cardItemImg;
    public Sprite soldImg;

    private ShopItemData itemData;

    public void Update()
    {
        UpdateImage(itemData);
    }
    public void SetCardUI(ShopItemData _data)
    {
        SetCardText(_data);
        
        SoldActive(_data);
    }

    public void SetCardText(ShopItemData _data)
    {
        titleTxt.text = _data.itemName;
        costTxt.text = _data.price.ToString();
    }

    public void SetCardImage(ShopItemData _data)
    {

    }

    public void UpdateImage(ShopItemData _data)
    {
        Debug.Log("UpdateImage �޼��� ȣ���");
        if (ShopManager.Instance.CanPurchaseItem(_data))
        {
           
        }
        else
        {
            cardItemImg.sprite = soldImg;
            Debug.Log("�ֵ�ƿ�����");
        }
    }

}
*/
