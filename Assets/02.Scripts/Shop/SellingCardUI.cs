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
    public Image cardGemImg;
    public Sprite gemImg;
    public Sprite specialGemImg;
    private ShopItemData itemData;

    public void SetCardUI(ShopItemData _data)
    {
        SetDefaultImg(_data);
        itemData = _data;
        UpdateText(_data);
        //UpdateImage();
        ShopManager.Instance.OnPurchaseStateChanged += OnPurchaseStateChanged; // ���� ���� ���� �̺�Ʈ ���
    }

    private void OnDestroy()
    {
        // �̺�Ʈ ����
       
        if (ShopManager.Instance != null)
            ShopManager.Instance.OnPurchaseStateChanged -= OnPurchaseStateChanged;
    }

    private void SetDefaultImg(ShopItemData _data)
    {
        Sprite[] shopitemimgs = Resources.LoadAll<Sprite>("ShopItems/ShopItemImgs");

        if (ShopManager.Instance.CanClickItem(_data))
        {
            foreach (var img in shopitemimgs)
            {
                if (img.name.Equals(_data.itemName))
                {
                    cardItemImg.sprite = img;
                    Debug.Log("SetDefaultImg : " + img.name);
                }
                else
                {
                    Debug.Log("��ġ�ϴ� ������ ������");
                }
            }
        }
        else
        {
            cardItemImg.sprite = soldImg; // �ֵ�ƿ� �̹����� ����
            Debug.Log("�⺻->�ֵ�ƿ�");
        }
    }

    void Update()
    {
        UpdateImage();
    }

    private void UpdateText(ShopItemData _data)
    {
        titleTxt.text = _data.itemName;
        costTxt.text = _data.price.ToString();
        UpdateGemImage(_data);
    }

    private void UpdateGemImage(ShopItemData _data)
    {
        if(_data.gemType== GemType.NORMAL)
        {
            cardGemImg.sprite = gemImg;
        }
        else
        {
            cardGemImg.sprite = specialGemImg;
        }
    }

    private void UpdateImage()
    {
        
        if (ShopManager.Instance.CanClickItem(itemData))
        {
            Sprite[] shopitemimgs = Resources.LoadAll<Sprite>("ShopItems/ShopItemImgs");
            foreach (var img in shopitemimgs)
            {
                if (img.name.Equals(itemData.itemName))
                {
                    cardItemImg.sprite = img;
                    Debug.Log("SetDefaultImg : " + img.name);
                }
                else
                {
                    Debug.Log("��ġ�ϴ� ������ ������");
                }
            }
            //cardItemImg.sprite = itemData.itemImg; // �⺻ �̹����� ���� //������ �ӽ÷� �� �ڵ���
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


