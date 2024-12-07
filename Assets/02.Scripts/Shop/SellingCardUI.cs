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
        ShopManager.Instance.OnPurchaseStateChanged += OnPurchaseStateChanged; // 구매 상태 변경 이벤트 등록
    }

    private void OnDestroy()
    {
        // 이벤트 해제
       
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
                    Debug.Log("일치하는 사진이 없습니");
                }
            }
        }
        else
        {
            cardItemImg.sprite = soldImg; // 솔드아웃 이미지로 설정
            Debug.Log("기본->솔드아웃");
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
                    Debug.Log("일치하는 사진이 없습니");
                }
            }
            //cardItemImg.sprite = itemData.itemImg; // 기본 이미지로 설정 //지금은 임시로 쓴 코드임
            Debug.Log("솔드아웃->기본");
        }
        else
        {
            cardItemImg.sprite = soldImg; // 솔드아웃 이미지로 설정
            Debug.Log("기본->솔드아웃");
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


