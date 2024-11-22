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
        ShopManager.Instance.OnPurchaseStateChanged += OnPurchaseStateChanged; // 구매 상태 변경 이벤트 등록
    }

    private void OnDestroy()
    {
        // 이벤트 해제
       
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
            cardItemImg.sprite = itemData.itemImg; // 기본 이미지로 설정 //지금은 임시로 쓴 코드임
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
        Debug.Log("UpdateImage 메서드 호출됨");
        if (ShopManager.Instance.CanPurchaseItem(_data))
        {
           
        }
        else
        {
            cardItemImg.sprite = soldImg;
            Debug.Log("솔드아웃사진");
        }
    }

}
*/
